namespace GCP.RazorPagesApp.Data.Entities;

public class Game
{
	public int Id { get; set; }
	public string Title { get; set; } = default!;
	public string NormalizedTitle { get; set; } = default!;

	public int MaxPlayers { get; set; }
	public DateOnly? ReleaseDate { get; set; }

	public bool SupportsOnlinePlay { get; set; }

	public List<GameEndorsement> Endorsements { get; set; } = new List<GameEndorsement>();
	public List<GameLink> GameLinks { get; set; } = new List<GameLink>();


	public List<Publisher> Publishers { get; set; } = new List<Publisher>();
	public List<GamePublisher> GamePublishers { get; set; } = new List<GamePublisher>();
	public List<GamePublisherPlatform> GamePublisherPlatforms { get; set; } = new List<GamePublisherPlatform>();


	public List<Genre> Genres { get; set; } = new List<Genre>();
	public List<GameGenre> GameGenres { get; set; } = new List<GameGenre>();


	public List<Retailer> Retailers { get; set; } = new List<Retailer>();
	public List<GameRetailer> GameRetailers { get; set; } = new List<GameRetailer>();
}
