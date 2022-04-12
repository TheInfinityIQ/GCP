
using Microsoft.AspNetCore.Identity;

namespace GCP.Api.Data.Entities;
public class User : IdentityUser<int>
{
	public string DisplayName { get; set; } = default!;
	public ICollection<Game> OwnedGames { get; set; } = new List<Game>();
}
