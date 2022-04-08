using GCP.Api.Data.Entities;
using GCP.Api.Data.Entities.Configuration;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GCP.Api.Data;

public class GCPContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
	public GCPContext(DbContextOptions<GCPContext> options)
		: base(options)
	{
	}

	public DbSet<Game> Game => Set<Game>();
	public DbSet<OwnedGame> OwnedGame => Set<OwnedGame>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
	}
}

