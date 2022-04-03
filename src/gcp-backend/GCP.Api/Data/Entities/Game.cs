namespace GCP.Api.Data.Entities;
public class Game
{
	public int Id { get; init; }
	public string Name { get; set; } = default!;
	public string NormalizedName { get; set; } = default!;
	public GameMetadata Metadata { get; init; } = new GameMetadata();
	public string? SteamAppId { get; set; }
	public DateOnly? ReleaseDate { get; set; }
}


public class GameMetadata
{
	public HashSet<string> Aliases { get; init; } = new HashSet<string>();
	public HashSet<string> Genres { get; init; } = new HashSet<string>();
	public HashSet<string> Platforms { get; init; } = new HashSet<string>();
	public HashSet<string> Launchers { get; init; } = new HashSet<string>();


	public string? IconUrl { get; set; }
	public GameImageUrlSet Card { get; init; } = new GameImageUrlSet();
	public GameImageUrlSet Background { get; init; } = new GameImageUrlSet();
}


public class GameImageUrlSet
{
	public string? Small { get; set; }
	public string? Medium { get; set; }
	public string? Large { get; set; }
}
