namespace GCP.RazorPagesApp.Data.Entities;

public class Genre
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string NormalizedName { get; set; } = default!;

	public List<Game> Games { get; set; } = new List<Game>();
	public List<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
}
