using GCP.RazorPagesApp.Data.Entities;

namespace GCP.RazorPagesApp.Api.Module.GameModule;

public record LinkDTO(int Id, Uri Url, LinkType Type, string? Label);
public record EndorsementDTO(int Id, int UserId, string User, double Weight);
public record NameRecordDTO(int Id, string Name);

public record PublisherDTO(int Id, string Name, IEnumerable<NameRecordDTO> Platforms)
	: NameRecordDTO(Id, Name);


public record GameListItemDTO(
	int Id,
	string Title,
	DateOnly? ReleaseDate,
	int MaxPlayers,
	bool SupportsOnlinePlay,
	IEnumerable<NameRecordDTO> Genres,
	IEnumerable<NameRecordDTO> Retailers,
	IEnumerable<PublisherDTO> Publishers,
	IEnumerable<LinkDTO> Links,
	IEnumerable<EndorsementDTO> Endorsements
)
{
	public GameListItemDTO(Game game)
		: this(
			game.Id,
			game.Title,
			game.ReleaseDate,
			game.MaxPlayers,
			game.SupportsOnlinePlay,
			game.Genres.Select(g => new NameRecordDTO(g.Id, g.Name)),
			game.Retailers.Select(r => new NameRecordDTO(r.Id, r.Name)),
			game.GamePublishers.Select(gp => new PublisherDTO(gp.PublisherId, gp.Publisher.Name, gp.Platforms.Select(p => new NameRecordDTO(p.Id, p.Name)))),
			game.GameLinks.Select(gl => new LinkDTO(gl.Id, gl.Link, gl.Type, gl.Label)),
			game.Endorsements.Select(e => new EndorsementDTO(e.Id, e.UserId, e.User.DisplayName ?? e.User.UserName, e.Weight))
		)
	{
	}
}

public record CreateGameDTO(
	string Title,
	DateOnly? ReleaseDate,
	int MaxPlayers,
	bool SupportsOnlinePlay,
	EndorsementDTO? Endorsement,
	IEnumerable<NameRecordDTO> Genres,
	IEnumerable<NameRecordDTO> Retailers,
	IEnumerable<PublisherDTO> Publishers,
	IEnumerable<LinkDTO> Links
)
{
}

public record UpdateGameDTO(
	string Title,
	DateOnly? ReleaseDate,
	int MaxPlayers,
	bool SupportsOnlinePlay,
	IEnumerable<EndorsementDTO> Endorsements,
	IEnumerable<NameRecordDTO> Genres,
	IEnumerable<NameRecordDTO> Retailers,
	IEnumerable<PublisherDTO> Publishers,
	IEnumerable<LinkDTO> Links
)
{
}
