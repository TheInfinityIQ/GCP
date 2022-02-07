using GCP.RazorPagesApp.Data.Entities;
using GCP.RazorPagesApp.Data.Entities.Configuration;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GCP.RazorPagesApp.Data;

public class GCPContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
	public GCPContext(DbContextOptions<GCPContext> options)
		: base(options)
	{
	}

	public DbSet<Game> Game { get; set; } = default!;

	public DbSet<GameEndorsement> GameEndorsement { get; set; } = default!;
	public DbSet<GameLink> GameLink { get; set; } = default!;


	public DbSet<Publisher> Publisher { get; set; } = default!;
	public DbSet<GamePublisher> GamePublisher { get; set; } = default!;

	public DbSet<Genre> Genre { get; set; } = default!;
	public DbSet<GameGenre> GameGenre { get; set; } = default!;


	public DbSet<Platform> Platform { get; set; } = default!;
	public DbSet<GamePublisherPlatform> GamePublisherPlatform { get; set; } = default!;

	public DbSet<Retailer> Retailer { get; set; } = default!;
	public DbSet<GameRetailer> GameRetailer { get; set; } = default!;


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresEnum<LinkType>();

		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
	}
}

