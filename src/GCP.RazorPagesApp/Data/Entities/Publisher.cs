namespace GCP.RazorPagesApp.Data.Entities;

public class Publisher
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string NormalizedName { get; set; } = default!;

	public List<Game> Games { get; set; } = new List<Game>();
	public List<GamePublisher> GamePublishers { get; set; } = new List<GamePublisher>();
}
