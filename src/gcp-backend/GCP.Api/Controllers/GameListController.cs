using GCP.Api.Data.Entities;
using GCP.Api.DTOs;
using GCP.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GCP.Api.Controllers;

public class GameListController : ApiController<GameListController>
{
	private readonly IGameListSerivce _gameListSerivce;

	public GameListController(
		ILogger<GameListController> logger,
		IHostEnvironment environment,
		IConfiguration configuration,
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		SignInManager<User> signInManager,
		IGameListSerivce gameListSerivce)
		: base(logger, environment, configuration, userManager, roleManager, signInManager)
	{
		_gameListSerivce = gameListSerivce;
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<ActionResult<GameListsResponseDTO>> Get(bool? hasDiscord = null, DateTimeOffset? activeFrom = null, CancellationToken cancellationToken = default)
	{
		var result = await _gameListSerivce.SearchAsync(new GameListSearchRequestDTO(UserId, hasDiscord, activeFrom), cancellationToken);
		return HandleResult(result);
	}

	[HttpGet("{id:int}")]
	[AllowAnonymous]
	public async Task<ActionResult<GameListDTO>> Get(int id, CancellationToken cancellationToken = default)
	{
		var result = await _gameListSerivce.DetailsAsync(new(id), cancellationToken);
		return HandleResult(result);
	}

	[HttpPost]
	public async Task<ActionResult<GameListDTO>> Post(GameListCreateRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var result = await _gameListSerivce.CreateAsync(requestDTO with { OwnerId = UserId.GetValueOrDefault() }, cancellationToken);
		return HandleResult(result, (r) => CreatedAtAction(nameof(Get), new { id = r.Content!.Id }, r.Content));
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult<GameListsResponseDTO>> Put(int id, GameListUpdateRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var result = await _gameListSerivce.UpdateAsync(requestDTO with { Id = id, OwnerId = UserId.GetValueOrDefault() }, cancellationToken);
		return HandleResult(result);
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult<GameListsResponseDTO>> Delete(int id, CancellationToken cancellationToken = default)
	{
		var result = await _gameListSerivce.DeleteAsync(new(id, UserId.GetValueOrDefault()), cancellationToken);
		return HandleResult(result);
	}
}

