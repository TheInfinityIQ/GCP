namespace GCP.Api.Data.Seeding;

public enum DatabaseMigrationOption
{
	None = 0,
	Drop = 1,
	Create = 2,
	Migrate = 4,
}
