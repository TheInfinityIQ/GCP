using GCP.Api.Data.Entities;
using GCP.Api.DTOs;
using GCP.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GCP.Api.Controllers;

public class AccountController : ApiController<AccountController>
{
	private readonly IAccountService _accountService;

	public AccountController(
		ILogger<AccountController> logger,
		IHostEnvironment environment,
		IConfiguration configuration,
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		SignInManager<User> signInManager,
		IAccountService accountService)
		: base(logger, environment, configuration, userManager, roleManager, signInManager)
	{
		_accountService = accountService;
	}

	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<ActionResult> Register(RegisterRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var result = await _accountService.RegisterAsync(requestDTO, cancellationToken);
		return HandleResult(result);
	}

	[HttpPost("/token")]
	[AllowAnonymous]
	public async Task<ActionResult<TokenResponseDTO>> Token(
		TokenRequestDTO requestDTO,
		CancellationToken cancellationToken = default)
	{
		var result = await _accountService.GetTokenAsync(requestDTO, cancellationToken);
		return HandleResult(result);
	}
}

