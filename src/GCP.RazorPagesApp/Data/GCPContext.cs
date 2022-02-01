using GCP.RazorPagesApp.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace GCP.RazorPagesApp.Data;

public class GCPContext : DbContext
{
	public GCPContext(DbContextOptions<GCPContext> options) : base(options)
	{

	}

	public DbSet<Game> Game { get; set; } = default!; // NOTE: = default! is to remove warnings
}
