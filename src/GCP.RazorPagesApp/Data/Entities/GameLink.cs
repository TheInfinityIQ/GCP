namespace GCP.RazorPagesApp.Data.Entities;

public class GameLink
{
	public int Id { get; set; }
	public int GameId { get; set; }
	public Game Game { get; set; } = default!;

	public string? Label { get; set; }

	public LinkType Type { get; set; }

	public Uri Link { get; set; } = default!;
}

public enum LinkType
{
	Unknown,
	Article,
	Store,
	Image,
	Video,
}
