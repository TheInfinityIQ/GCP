using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GCP.Api.Controllers;

public class SecretController : ApiController
{
	[HttpGet]
	public ActionResult<SecretDTO> GetSecret() => Ok(new SecretDTO("My very secret value."));

	[HttpGet("public")]
	[AllowAnonymous]
	public ActionResult<SecretDTO> GetPublicSecret() => Ok(new SecretDTO("My not well kept secret value."));
}


public record SecretDTO(string Value);
