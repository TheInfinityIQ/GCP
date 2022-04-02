#nullable disable

using System.ComponentModel.DataAnnotations;

using GCP.Api.Data;
using GCP.Api.Data.Entities;
using GCP.Api.Data.Entities.Configuration;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GCP.Api.Areas.Identity.Pages.Account.Manage
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly GCPContext _context;

		public IndexModel(
			UserManager<User> userManager,
			SignInManager<User> signInManager,
			GCPContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}

		public string Username { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Phone]
			[Display(Name = "Phone number")]
			public string PhoneNumber { get; set; }

			[Required]
			[StringLength(UserConfiguration.DISPLAY_NAME_MAX_LENGTH, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[Display(Name = "Display name")]
			public string DisplayName { get; set; }
		}

		private async Task LoadAsync(User user)
		{
			var userName = await _userManager.GetUserNameAsync(user);
			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

			Username = userName;

			Input = new InputModel
			{
				PhoneNumber = phoneNumber,
				DisplayName = user.DisplayName,
			};
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user is null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			await LoadAsync(user);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user is null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			if (!ModelState.IsValid)
			{
				await LoadAsync(user);
				return Page();
			}

			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			if (Input.PhoneNumber != phoneNumber)
			{
				var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
				if (!setPhoneResult.Succeeded)
				{
					StatusMessage = "Unexpected error when trying to set phone number.";
					return RedirectToPage();
				}
			}

			Input.DisplayName = Input.DisplayName.Trim();
			if (Input.DisplayName != user.DisplayName)
			{
				if (await _context.Users.Where(u => u.Id != user.Id).AnyAsync(u => u.DisplayName.ToLower() == Input.DisplayName.ToLower(), cancellationToken))
				{
					ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.DisplayName)}", "Display name is already taken!");
					await LoadAsync(user);
					return Page();
				}
				user.DisplayName = Input.DisplayName;

				await _context.SaveChangesAsync(cancellationToken);
			}

			await _signInManager.RefreshSignInAsync(user);
			StatusMessage = "Your profile has been updated";
			return RedirectToPage();
		}
	}
}
