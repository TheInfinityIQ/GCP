using GCP.RazorPagesApp.Data.Entities;

namespace GCP.RazorPagesApp.Api.Module.GameModule;

public record Link(Uri Url, LinkType Type, string? Label);
public record Endorsement(string User, double Weight);

public record Publisher(string Name, IEnumerable<string> Platforms);

public record GameListItemDTO(
	int Id,
	string Title,
	DateOnly? ReleaseDate,
	int MaxPlayers,
	bool SupportsOnlinePlay,
	IEnumerable<string> Genres,
	IEnumerable<string> Platforms,
	IEnumerable<string> Retailers,
	IEnumerable<Publisher> Publishers,
	IEnumerable<Link> Links,
	IEnumerable<Endorsement> Endorsements
)
{
	public GameListItemDTO(Game game)
		: this(
			game.Id,
			game.Title,
			game.ReleaseDate,
			game.MaxPlayers,
			game.SupportsOnlinePlay,
			game.Genres.Select(g => g.Name),
			game.GamePublisherPlatforms.Select(gpp => gpp.Platform.Name),
			game.Retailers.Select(r => r.Name),
			game.GamePublishers.Select(gp => new Publisher(gp.Publisher.Name, gp.Platforms.Select(p => p.Name))),
			game.GameLinks.Select(gl => new Link(gl.Link, gl.Type, gl.Label)),
			game.Endorsements.Select(e => new Endorsement(e.User.DisplayName ?? e.User.UserName, e.Weight))
		)
	{
	}
}

public record CreateGameDTO(string Link, int Score, string User)
{
}
