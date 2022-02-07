
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class GamePublisherPlatformConfiguration : IEntityTypeConfiguration<GamePublisherPlatform>
{
	public void Configure(EntityTypeBuilder<GamePublisherPlatform> builder)
	{
		builder.HasKey(gpp => new { gpp.GameId, gpp.PublisherId, gpp.PlatformId });

		builder.UseXminAsConcurrencyToken();
	}
}
