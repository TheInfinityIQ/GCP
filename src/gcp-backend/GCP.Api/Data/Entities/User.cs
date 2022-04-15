
using Microsoft.AspNetCore.Identity;

namespace GCP.Api.Data.Entities;
public class User : IdentityUser<int>
{
	public string DisplayName { get; set; } = default!;
	public ICollection<Game> OwnedGames { get; init; } = new List<Game>();
	public ICollection<GameList> OwnedGameLists { get; init; } = new List<GameList>();
	public ICollection<GameList> JoinedGameLists { get; init; } = new List<GameList>();
}
