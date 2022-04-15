using GCP.Api.Data.Entities;
using GCP.Api.Utilities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GCP.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public abstract class ApiController<T> : Controller
	where T : ApiController<T>
{
	protected readonly ILogger<T> _logger;
	protected readonly IHostEnvironment _environment;
	protected readonly IConfiguration _configuration;
	protected readonly UserManager<User> _userManager;
	protected readonly RoleManager<Role> _roleManager;
	protected readonly SignInManager<User> _signInManager;

	protected int? UserId => GetUserIdOrNull();
	protected bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;

	public ApiController(ILogger<T> logger, IHostEnvironment environment, IConfiguration configuration, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
	{
		_logger = logger;
		_environment = environment;
		_configuration = configuration;
		_userManager = userManager;
		_roleManager = roleManager;
		_signInManager = signInManager;
	}

	protected bool TryGetUserId(out int userId) => int.TryParse(_userManager.GetUserId(User), out userId);
	protected int? GetUserIdOrNull() => TryGetUserId(out var userId) ? userId : null;

	protected ModelStateDictionary AddEmptyModelError(string errorMessage)
	{
		ModelState.AddModelError(string.Empty, errorMessage);
		return ModelState;
	}

	protected ModelStateDictionary AddGCPResult(GCPResult result)
	{
		if (result.Succeeded)
		{
			return ModelState;
		}

		foreach (var (errorCode, message) in result.Errors)
		{
			var key = (errorCode, (int)errorCode) switch
			{
				var (code, number) when number >= GCPErrorCodeExtensions.DomainErrorStartingPoint => $"B_{number}",
				var (code, number) => $"E_{number}",
			};
			ModelState.AddModelError(key, message ?? "An error has occurred.");
		}

		return ModelState;
	}

	protected virtual ActionResult HandleResult(GCPResult result)
	{
		if (result.Failed)
		{
			return HandleErrorResult(result);
		}

		return NoContent();
	}

	protected virtual ActionResult HandleResult(GCPResult result, Func<GCPResult, ActionResult> successHandler)
	{
		if (result.Failed)
		{
			return HandleErrorResult(result);
		}

		return successHandler(result);
	}

	protected virtual ActionResult<TContent> HandleResult<TContent>(GCPResult<TContent> result)
	{
		if (result.Failed)
		{
			return HandleErrorResult(result);
		}

		return Ok(result.Content);
	}

	protected virtual ActionResult<TContent> HandleResult<TContent>(GCPResult<TContent> result, Func<GCPResult<TContent>, ActionResult<TContent>> successHandler)
	{
		if (result.Failed)
		{
			return HandleErrorResult(result);
		}

		return successHandler(result);
	}

	protected virtual ActionResult HandleErrorResult(GCPResult result)
	{
		foreach (var (errorCode, message) in result.Errors)
		{
			switch (errorCode)
			{
				case GCPErrorCode.InternalServerError: return Problem(detail: "unexpected error occurred.", statusCode: 500, title: "Internal Server Error");
				case GCPErrorCode.Unauthorized: return Unauthorized();
				case GCPErrorCode.Forbidden: return Forbid();
				case GCPErrorCode.NotFound: return NotFound();
				case GCPErrorCode.Conflict: return Conflict();
				case GCPErrorCode.NotImplemented: return BadRequest(AddEmptyModelError($"API not yet implemented. {message}".Trim()));
			}
		}

		AddGCPResult(result);
		return BadRequest(ModelState);
	}
}
