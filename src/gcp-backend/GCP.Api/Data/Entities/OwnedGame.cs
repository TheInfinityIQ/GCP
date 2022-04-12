namespace GCP.Api.Data.Entities;
public class OwnedGame
{
	public int GameId { get; init; }
	public Game Game { get; init; } = default!;

	public int UserId { get; init; }
	public User User { get; init; } = default!;
}
