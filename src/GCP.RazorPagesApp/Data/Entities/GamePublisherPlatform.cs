namespace GCP.RazorPagesApp.Data.Entities;

public class GamePublisherPlatform
{
	public int GameId { get; set; }
	public int PublisherId { get; set; }
	public GamePublisher? GamePublisher { get; set; }

	public int PlatformId { get; set; }
	public Platform Platform { get; set; } = default!;
}
