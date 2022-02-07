namespace GCP.RazorPagesApp.Data.Entities;

public class GameEndorsement
{
	public int Id { get; set; }
	public int GameId { get; set; }
	public Game Game { get; set; } = default!;

	public int UserId { get; set; }
	public User User { get; set; } = default!;

	public double Weight { get; set; }
}
