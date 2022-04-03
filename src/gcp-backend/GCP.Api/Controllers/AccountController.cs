using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using GCP.Api.Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GCP.Api.Controllers;

public class AccountController : ApiController
{
	private readonly ILogger<AccountController> _logger;
	private readonly IConfiguration _configuration;
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<Role> _roleManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IUserStore<User> _userStore;
	private readonly IUserEmailStore<User> _emailStore;
	private readonly IEmailSender _emailSender;

	public AccountController(
		ILogger<AccountController> logger,
		IConfiguration configuration,
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		SignInManager<User> signInManager,
		IUserStore<User> userStore,
		IEmailSender emailSender)
	{
		_logger = logger;
		_configuration = configuration;
		_userManager = userManager;
		_roleManager = roleManager;
		_signInManager = signInManager;
		_userStore = userStore;
		_emailStore = GetEmailStore();
		_emailSender = emailSender;
	}

	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<ActionResult> Register(RegisterRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		//var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

		requestDTO = requestDTO with
		{
			Email = requestDTO.Email.Trim(),
			DisplayName = requestDTO.DisplayName.Trim(),
		};

		if (await _userManager.FindByEmailAsync(requestDTO.Email) is not null)
		{
			ModelState.AddModelError<RegisterRequestDTO>(
				  static x => x.Email,
				  "Email is already taken, try a different one.");
			return BadRequest(ModelState);
		}

		var isDisplayNameAlreadyTaken = await _userManager.Users
			.AnyAsync(u => u.DisplayName.ToLower() == requestDTO.DisplayName.ToLower(), cancellationToken);
		if (isDisplayNameAlreadyTaken)
		{
			ModelState.AddModelError<RegisterRequestDTO>(
				static x => x.DisplayName,
				"Display name is already taken, try a different one.");
			return BadRequest(ModelState);
		}

		var user = CreateUser();
		user.DisplayName = requestDTO.DisplayName;

		await _userStore.SetUserNameAsync(user, requestDTO.Email, cancellationToken);
		await _emailStore.SetEmailAsync(user, requestDTO.Email, cancellationToken);
		var result = await _userManager.CreateAsync(user, requestDTO.Password);

		if (!result.Succeeded)
		{
			foreach (var error in result.Errors)
			{
				AddEmptyModelError(error.Description);
			}
			return BadRequest(ModelState);
		}

		_logger.LogInformation("User created a new account with password.");

		//var userId = await _userManager.GetUserIdAsync(user);
		//var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		//code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
		//var callbackUrl = Url.Page(
		//	"/Account/ConfirmEmail",
		//	pageHandler: null,
		//	values: new { area = "Identity", userId, code, returnUrl },
		//	protocol: Request.Scheme);

		//await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
		//	$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

		//if (_userManager.Options.SignIn.RequireConfirmedAccount)
		//{
		//	return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
		//}
		//else
		//{
		await _signInManager.SignInAsync(user, isPersistent: false);
		//return LocalRedirect(returnUrl);
		//}


		var tokenRequest = new TokenRequestDTO(user.Email, requestDTO.Password);
		var response = await Token(tokenRequest, cancellationToken);
		return response.Result!;
	}

	[HttpPost("/token")]
	[AllowAnonymous]
	public async Task<ActionResult<TokenResponseDTO>> Token(
		TokenRequestDTO requestDTO,
		CancellationToken cancellationToken = default)
	{
		//var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

		// This doesn't count login failures towards account lockout
		// To enable password failures to trigger account lockout, set lockoutOnFailure: true
		var result = await _signInManager.PasswordSignInAsync(requestDTO.Email, requestDTO.Password, requestDTO.RememberMe, lockoutOnFailure: false);
		if (result.Succeeded)
		{
			_logger.LogInformation("User logged in.");
			//return LocalRedirect(returnUrl);

			var user = await _userManager.FindByNameAsync(requestDTO.Email);
			if (user is null || !await _userManager.CheckPasswordAsync(user, requestDTO.Password))
			{
				return Unauthorized();
			}
			var userRoles = await _userManager.GetRolesAsync(user);

			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.DisplayName),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			foreach (var userRole in userRoles)
			{
				authClaims.Add(new Claim(ClaimTypes.Role, userRole));
			}

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));
			var expiryTimeSpan = _configuration.GetValue("JWT:ExpiryTimeSpan", TimeSpan.FromMinutes(30));
			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.Add(expiryTimeSpan),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);

			var handler = new JwtSecurityTokenHandler();

			return Ok(new TokenResponseDTO
			{
				AccessToken = handler.WriteToken(token),
				ExpiriesOn = token.ValidTo,
			});
		}


		if (result.RequiresTwoFactor)
		{
			throw new NotImplementedException();
			//return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, requestDTO.RememberMe });
		}

		if (result.IsLockedOut)
		{
			_logger.LogWarning("User account locked out.");
			//return RedirectToPage("./Lockout");
			AddEmptyModelError("User account locked out.");
			return Unauthorized(ModelState);
		}
		else
		{
			AddEmptyModelError("Invalid login attempt.");
			return BadRequest(ModelState);
		}
	}





	private User CreateUser()
	{
		try
		{
			return Activator.CreateInstance<User>();
		}
		catch
		{
			throw new InvalidOperationException(
				$"Can't create an instance of '{nameof(User)}'. " +
				$"Ensure that '{nameof(User)}' is not an abstract class " +
				$"and has a parameterless constructor, or alternatively " +
				$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
		}
	}

	private IUserEmailStore<User> GetEmailStore()
	{
		if (!_userManager.SupportsUserEmail)
		{
			throw new NotSupportedException("The default UI requires a user store with email support.");
		}
		return (IUserEmailStore<User>)_userStore;
	}
}


public record RegisterRequestDTO(string Email, string DisplayName, string Password);

public record TokenRequestDTO(string Email, string Password, bool RememberMe = false);
public record TokenResponseDTO
{
	//[JsonPropertyName("access_token")]
	public string AccessToken { get; init; } = default!;

	//[JsonPropertyName("expiries_on")]
	public DateTimeOffset ExpiriesOn { get; init; }
}
