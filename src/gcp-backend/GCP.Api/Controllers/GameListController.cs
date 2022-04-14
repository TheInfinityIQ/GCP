using GCP.Api.Data;
using GCP.Api.Data.Entities;
using GCP.Api.DTOs;
using GCP.Api.Utilities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GCP.Api.Controllers;

public class GameListController : ApiController<GameListController>
{
	private readonly GCPContext _context;

	public GameListController(
		ILogger<GameListController> logger,
		IHostEnvironment environment,
		IConfiguration configuration,
		UserManager<User> userManager,
		RoleManager<Role> roleManager,
		SignInManager<User> signInManager,
		GCPContext context)
		: base(logger, environment, configuration, userManager, roleManager, signInManager)
	{
		_context = context;
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<ActionResult<GameListsResponseDTO>> Get(CancellationToken cancellationToken = default)
	{
		var gameLists = await _context.GameList
			.AsNoTracking()
			.Select(gl => new GameListDTO(
				gl.Id,
				new(gl.OwnerId, gl.Owner.DisplayName),
				gl.Title,
				gl.Description,
				gl.VoteOncePerGame,
				gl.IsPublic,
				gl.UserLimit,
				gl.CreatedOnUtc,
				gl.LastUpdatedOnUtc,
				gl.Users.Select(u => new UserDisplayNameDTO(u.Id, u.DisplayName))
			))
			.ToListAsync(cancellationToken);

		return Ok(new GameListsResponseDTO(gameLists));
	}


	[HttpGet("{id:int}")]
	[AllowAnonymous]
	public async Task<ActionResult<GameListsResponseDTO>> Get(int id, CancellationToken cancellationToken = default)
	{
		var gameList = await _context.GameList
			.AsNoTracking()
			.Select(gl => new GameListDTO(
				gl.Id,
				new(gl.OwnerId, gl.Owner.DisplayName),
				gl.Title,
				gl.Description,
				gl.VoteOncePerGame,
				gl.IsPublic,
				gl.UserLimit,
				gl.CreatedOnUtc,
				gl.LastUpdatedOnUtc,
				gl.Users.Select(u => new UserDisplayNameDTO(u.Id, u.DisplayName))
			))
			.FirstOrDefaultAsync(gl => gl.Id == id, cancellationToken);
		if (gameList is null)
		{
			return NotFound();
		}

		return Ok(gameList);
	}


	[HttpPost]
	public async Task<ActionResult<GameListsResponseDTO>> Post(GameListCreateRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var currentUser = await _userManager.GetUserAsync(User);
		if (currentUser is null)
		{
			return Unauthorized();
		}

		var normalizedTitle = requestDTO.Title.ToUpperInvariant();

		var isTitleTaken = await _context.GameList.AnyAsync(gl => gl.NormalizedTitle == normalizedTitle, cancellationToken);
		if (isTitleTaken)
		{
			var result = GCPResult.Failure(GCPErrorCode.TitleIsAlreadyTaken, "title is already taken");
			return HandleErrorResult(result);
		}
		var gameList = new GameList
		{
			OwnerId = currentUser.Id,
			Title = requestDTO.Title,
			NormalizedTitle = normalizedTitle,
			Description = requestDTO.Description,
			VoteOncePerGame = requestDTO.VoteOncePerGame,
			UserLimit = requestDTO.UserLimit,
			IsPublic = requestDTO.IsPublic,
		};

		await _context.GameList.AddAsync(gameList, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		var responseDTO = new GameListDTO(
			gameList.Id,
			new(gameList.OwnerId, gameList.Owner.DisplayName),
			gameList.Title,
			gameList.Description,
			gameList.VoteOncePerGame,
			gameList.IsPublic,
			gameList.UserLimit,
			gameList.CreatedOnUtc,
			gameList.LastUpdatedOnUtc,
			gameList.Users.Select(u => new UserDisplayNameDTO(u.Id, u.DisplayName))
		);

		return CreatedAtAction(nameof(Get), new { id = responseDTO.Id }, responseDTO);
	}


	[HttpPut("{id:int}")]
	public async Task<ActionResult<GameListsResponseDTO>> Put(int id, GameListUpdateRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var currentUser = await _userManager.GetUserAsync(User);
		if (currentUser is null)
		{
			return Unauthorized();
		}

		var gameList = await _context.GameList
			.Include(gl => gl.Owner)
			.Include(gl => gl.Users)
			.FirstOrDefaultAsync(gl => gl.Id == id, cancellationToken);
		if (gameList is null)
		{
			return NotFound();
		}

		if (gameList.OwnerId != currentUser.Id)
		{
			return Unauthorized();
		}

		var normalizedTitle = requestDTO.Title.ToUpperInvariant();

		var isTitleTaken = await _context.GameList.AnyAsync(gl => gl.Id != id && gl.NormalizedTitle == normalizedTitle, cancellationToken);
		if (isTitleTaken)
		{
			var result = GCPResult.Failure(GCPErrorCode.TitleIsAlreadyTaken, "title is already taken");
			return HandleErrorResult(result);
		}
		gameList.Title = requestDTO.Title;
		gameList.NormalizedTitle = normalizedTitle;
		gameList.Description = requestDTO.Description;
		gameList.VoteOncePerGame = requestDTO.VoteOncePerGame;
		gameList.UserLimit = requestDTO.UserLimit;
		gameList.IsPublic = requestDTO.IsPublic;

		var userIds = requestDTO.Users.Where(u => u != currentUser.Id).ToHashSet();
		var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync(cancellationToken);
		foreach (var user in users)
		{
			if (!gameList.Users.Any(u => u.Id != user.Id))
			{
				gameList.Users.Add(user);
				continue;
			}

			gameList.Users.Remove(user);
		}

		gameList.LastUpdatedOnUtc = DateTimeOffset.UtcNow;

		await _context.SaveChangesAsync(cancellationToken);

		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult<GameListsResponseDTO>> Delete(int id, CancellationToken cancellationToken = default)
	{
		var currentUser = await _userManager.GetUserAsync(User);
		if (currentUser is null)
		{
			return Unauthorized();
		}

		var gameList = await _context.GameList.FirstOrDefaultAsync(gl => gl.Id == id, cancellationToken);
		if (gameList is null)
		{
			return NotFound();
		}

		if (gameList.OwnerId != currentUser.Id)
		{
			return Unauthorized();
		}

		_context.GameList.Remove(gameList);
		await _context.SaveChangesAsync(cancellationToken);

		return NoContent();
	}
}

