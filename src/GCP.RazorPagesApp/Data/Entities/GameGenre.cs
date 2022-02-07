namespace GCP.RazorPagesApp.Data.Entities;

public class GameGenre
{
	public int GameId { get; set; }
	public Game Game { get; set; } = default!;

	public int GenreId { get; set; }
	public Genre Genre { get; set; } = default!;
}
