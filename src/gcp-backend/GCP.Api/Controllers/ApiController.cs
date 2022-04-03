using Microsoft.AspNetCore.Mvc;

namespace GCP.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public abstract class ApiController : Controller
{
}
