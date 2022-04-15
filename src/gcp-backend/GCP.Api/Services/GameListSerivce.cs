using GCP.Api.Data;
using GCP.Api.Data.Entities;
using GCP.Api.DTOs;
using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;

namespace GCP.Api.Services;

public interface IGameListSerivce
{
	Task<GCPResult<GameListDTO>> CreateAsync(GameListCreateRequestDTO requestDTO, CancellationToken cancellationToken = default);
	Task<GCPResult> DeleteAsync(GameListDeleteRequestDTO requestDTO, CancellationToken cancellationToken = default);
	Task<GCPResult<GameListDTO>> DetailsAsync(GameListDetailsRequestDTO requestDTO, CancellationToken cancellationToken = default);
	Task<GCPResult<GameListsResponseDTO>> SearchAsync(GameListSearchRequestDTO requestDTO, CancellationToken cancellationToken = default);
	Task<GCPResult> UpdateAsync(GameListUpdateRequestDTO requestDTO, CancellationToken cancellationToken = default);
}

public class GameListSerivce : IGameListSerivce
{
	private readonly ILogger<GameListSerivce> _logger;
	private readonly GCPContext _context;

	public GameListSerivce(ILogger<GameListSerivce> logger, GCPContext context)
	{
		_logger = logger;
		_context = context;
		_ = _logger;
	}

	public async Task<GCPResult<GameListsResponseDTO>> SearchAsync(GameListSearchRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		_ = requestDTO; //TODO: search logic
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

		return GCPResult.Success(new GameListsResponseDTO(gameLists));
	}

	public async Task<GCPResult<GameListDTO>> DetailsAsync(GameListDetailsRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var gameList = await _context.GameList
			.AsNoTracking()
			.Where(gl => gl.Id == requestDTO.Id)
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
			.FirstOrDefaultAsync(cancellationToken);
		if (gameList is null)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.NotFound);
		}

		return GCPResult.Success(gameList);
	}

	public async Task<GCPResult<GameListDTO>> CreateAsync(GameListCreateRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == requestDTO.OwnerId, cancellationToken);
		if (currentUser is null)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.Unauthorized);
		}

		var normalizedTitle = requestDTO.Title.ToUpperInvariant();

		var isTitleTaken = await _context.GameList.AnyAsync(gl => gl.NormalizedTitle == normalizedTitle, cancellationToken);
		if (isTitleTaken)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.TitleIsAlreadyTaken, "title is already taken");
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

		return GCPResult.Success(responseDTO);
	}

	public async Task<GCPResult> UpdateAsync(GameListUpdateRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == requestDTO.OwnerId, cancellationToken);
		if (currentUser is null)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.Unauthorized);
		}

		var gameList = await _context.GameList
			.Include(gl => gl.Owner)
			.Include(gl => gl.Users)
			.FirstOrDefaultAsync(gl => gl.Id == requestDTO.Id, cancellationToken);
		if (gameList is null)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.NotFound);
		}

		if (gameList.OwnerId != currentUser.Id)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.Unauthorized);
		}

		var normalizedTitle = requestDTO.Title.ToUpperInvariant();

		var isTitleTaken = await _context.GameList.AnyAsync(gl => gl.Id != requestDTO.Id && gl.NormalizedTitle == normalizedTitle, cancellationToken);
		if (isTitleTaken)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.TitleIsAlreadyTaken, "title is already taken");
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

		return GCPResult.Success();
	}

	public async Task<GCPResult> DeleteAsync(GameListDeleteRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == requestDTO.OwnerId, cancellationToken);
		if (currentUser is null)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.Unauthorized);
		}

		var gameList = await _context.GameList.FirstOrDefaultAsync(gl => gl.Id == requestDTO.Id, cancellationToken);
		if (gameList is null)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.NotFound);
		}

		if (gameList.OwnerId != currentUser.Id)
		{
			return GCPResult.Failure<GameListDTO>(GCPErrorCode.Unauthorized);
		}

		_context.GameList.Remove(gameList);
		await _context.SaveChangesAsync(cancellationToken);

		return GCPResult.Success();
	}
}
