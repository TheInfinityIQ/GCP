#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GCP.Api.Areas.Identity.Pages.Account.Manage
{
	public class ShowRecoveryCodesModel : PageModel
	{
		[TempData]
		public string[] RecoveryCodes { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		public IActionResult OnGet()
		{
			if (RecoveryCodes is null || RecoveryCodes.Length is 0)
			{
				return RedirectToPage("./TwoFactorAuthentication");
			}

			return Page();
		}
	}
}
