using GCP.Api.Data.Entities;
using GCP.Api.DTOs;
using GCP.Api.Services;

using Gameloop.Vdf;
using Gameloop.Vdf.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GCP.Api.Controllers;

[AllowAnonymous]
public class SteamController : ApiController<SteamController>
{
	private readonly ISteamSerivce _steamSerivce;

	public SteamController(
		ILogger<SteamController> logger,
		IHostEnvironment environment,
		IConfiguration configuration,
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		SignInManager<User> signInManager,
		ISteamSerivce steamSerivce)
		: base(logger, environment, configuration, userManager, roleManager, signInManager)
	{
		_steamSerivce = steamSerivce;
	}

	[HttpPost("parse-vdf")]
	public async Task<ActionResult<ParseVdfResponseDTO>> ParseVdf(IFormFile vdf, CancellationToken cancellationToken = default)
	{
		using var fs = vdf.OpenReadStream();
		var request = new ParseVdfRequestDTO(UserId, fs);
		var result = await _steamSerivce.ParseVdfAsync(request, cancellationToken);
		return HandleResult(result);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "failed to retrieve steam games");
			AddEmptyModelError("[INTERNAL] failed to retrieve steam games");
			return StatusCode(500, ModelState);
		}

	[HttpGet("app")]
	public async Task<ActionResult<IDictionary<long, string>>> Get(CancellationToken cancellationToken = default)
		{
		var result = await _steamSerivce.GetSteamAppListAsync(cancellationToken);
		return HandleResult(result);
		}

	[HttpGet("app/{id:long}")]
	public async Task<ActionResult<SteamAppDetailsDTO>> Get(long id, CancellationToken cancellationToken = default)
		{
		var result = await _steamSerivce.GetSteamAppDetailsAsync(id, cancellationToken);
		return HandleResult(result);
	}
}

