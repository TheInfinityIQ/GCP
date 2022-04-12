using GCP.Api.Data.Entities;
using GCP.Api.DTOs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GCP.Api.Controllers;

public class SecretController : ApiController<SecretController>
{
	public SecretController(
		ILogger<SecretController> logger,
		IHostEnvironment environment,
		IConfiguration configuration,
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		SignInManager<User> signInManager)
		: base(logger, environment, configuration, userManager, roleManager, signInManager)
	{
	}

	[HttpGet]
	public ActionResult<SecretResponseDTO> GetSecret() => Ok(new SecretResponseDTO("My very secret value."));

	[HttpGet("public")]
	[AllowAnonymous]
	public ActionResult<SecretResponseDTO> GetPublicSecret() => Ok(new SecretResponseDTO("My not well kept secret value."));
}

