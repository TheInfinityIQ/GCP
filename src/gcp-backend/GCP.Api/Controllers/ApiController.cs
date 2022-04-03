using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GCP.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public abstract class ApiController : Controller
{
	protected ModelStateDictionary AddEmptyModelError(string errorMessage)
	{
		ModelState.AddModelError(string.Empty, errorMessage);
		return ModelState;
	}
}
