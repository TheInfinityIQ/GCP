namespace GCP.RazorPagesApp.Data.Entities;

public class Platform
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string NormalizedName { get; set; } = default!;

	public List<GamePublisher> GamePublishers { get; set; } = new List<GamePublisher>();
	public List<GamePublisherPlatform> GamePublisherPlatforms { get; set; } = new List<GamePublisherPlatform>();
}
