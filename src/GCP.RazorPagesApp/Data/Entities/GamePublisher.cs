namespace GCP.RazorPagesApp.Data.Entities;

public class GamePublisher
{
	public int GameId { get; set; }
	public Game Game { get; set; } = default!;

	public int PublisherId { get; set; }
	public Publisher Publisher { get; set; } = default!;

	public List<Platform> Platforms { get; set; } = new List<Platform>();
	public List<GamePublisherPlatform> GamePublisherPlatforms { get; set; } = new List<GamePublisherPlatform>();
}
