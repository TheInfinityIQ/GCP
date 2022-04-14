namespace GCP.Api.Data.Entities;

public class GameListUser
{
	public int UserId { get; init; }
	public User User { get; init; } = default!;
	public int GameListId { get; init; }
	public GameList GameList { get; set; } = default!;

	public DateTimeOffset JoinedOnUtc { get; init; } = DateTimeOffset.UtcNow;
}


