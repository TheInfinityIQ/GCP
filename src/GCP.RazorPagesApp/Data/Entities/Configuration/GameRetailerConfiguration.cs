
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class GameRetailerConfiguration : IEntityTypeConfiguration<GameRetailer>
{
	public void Configure(EntityTypeBuilder<GameRetailer> builder)
	{
		builder.HasKey(gr => new { gr.GameId, gr.RetailerId });

		builder.UseXminAsConcurrencyToken();
	}
}
