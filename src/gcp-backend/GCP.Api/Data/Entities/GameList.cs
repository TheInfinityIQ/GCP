namespace GCP.Api.Data.Entities;
public class GameList
{
	public int Id { get; init; }
	public int OwnerId { get; init; }
	public User Owner { get; init; } = default!;

	public string Title { get; set; } = default!;
	public string NormalizedTitle { get; set; } = default!;
	public string? Description { get; set; }

	public bool VoteOncePerGame { get; set; }
	public bool IsPublic { get; set; }
	public int? UserLimit { get; set; }

	public DateTimeOffset CreatedOnUtc { get; init; } = DateTimeOffset.UtcNow;
	public DateTimeOffset? LastUpdatedOnUtc { get; set; }

	public ICollection<User> Users { get; init; } = new List<User>();
}


