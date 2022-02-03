#nullable disable

using GCP.RazorPagesApp.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GCP.RazorPagesApp.Areas.Identity.Pages.Account.Manage
{
	public class PersonalDataModel : PageModel
	{
		private readonly UserManager<User> _userManager;
		private readonly ILogger<PersonalDataModel> _logger;

		public PersonalDataModel(
			UserManager<User> userManager,
			ILogger<PersonalDataModel> logger)
		{
			_userManager = userManager;
			_logger = logger;
			_ = _logger; // NOTE: for ignoring message/warning
		}

		public async Task<IActionResult> OnGet()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user is null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			return Page();
		}
	}
}
