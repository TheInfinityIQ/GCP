using System.ComponentModel.DataAnnotations;

namespace GCP.Api.Data.Seeding;

public record SeederOptions
{
	public const string KEY = nameof(SeederOptions);

	[Required]
	public bool SkipSeed { get; set; }

	[Required]
	public HashSet<DatabaseMigrationOption> DatabaseMigrationOptions { get; init; } = new HashSet<DatabaseMigrationOption>();

	[Required]
	public string SharedTestPassword { get; init; } = default!;
}
