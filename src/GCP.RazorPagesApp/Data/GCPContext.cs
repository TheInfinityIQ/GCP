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

	public DbSet<Game> Game { get; set; } = default!; // NOTE: = default! is to remove warnings

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
	}
}

