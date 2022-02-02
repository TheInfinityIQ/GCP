using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GCP.RazorPagesApp.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
			_ = _logger; // NOTE: for ignoring message/warning
		}

		public void OnGet()
		{

		}
	}
}
