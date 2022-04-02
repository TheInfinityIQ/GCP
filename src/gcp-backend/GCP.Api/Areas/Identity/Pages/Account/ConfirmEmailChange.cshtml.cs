#nullable disable

using System.Text;

using GCP.Api.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace GCP.Api.Areas.Identity.Pages.Account
{
	public class ConfirmEmailChangeModel : PageModel
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public ConfirmEmailChangeModel(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
		{
			if (userId is null || email is null || code is null)
			{
				return RedirectToPage("/Index");
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user is null)
			{
				return NotFound($"Unable to load user with ID '{userId}'.");
			}

			code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			var result = await _userManager.ChangeEmailAsync(user, email, code);
			if (!result.Succeeded)
			{
				StatusMessage = "Error changing email.";
				return Page();
			}

			// In our UI email and user name are one and the same, so when we update the email
			// we need to update the user name.
			var setUserNameResult = await _userManager.SetUserNameAsync(user, email);
			if (!setUserNameResult.Succeeded)
			{
				StatusMessage = "Error changing user name.";
				return Page();
			}

			await _signInManager.RefreshSignInAsync(user);
			StatusMessage = "Thank you for confirming your email change.";
			return Page();
		}
	}
}
