using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GCP.RazorPagesApp.Pages
{
	public class PrivacyModel : PageModel
	{
		private readonly ILogger<PrivacyModel> _logger;

		public PrivacyModel(ILogger<PrivacyModel> logger)
		{
			_logger = logger;
			_ = _logger; // NOTE: for ignoring message/warning
		}

		public void OnGet()
		{
		}
	}
}
