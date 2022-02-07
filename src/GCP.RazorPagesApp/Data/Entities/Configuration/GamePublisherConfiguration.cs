
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class GamePublisherConfiguration : IEntityTypeConfiguration<GamePublisher>
{
	public void Configure(EntityTypeBuilder<GamePublisher> builder)
	{
		builder.HasKey(gp => new { gp.GameId, gp.PublisherId });

		builder.UseXminAsConcurrencyToken();

		builder.HasMany(gp => gp.Platforms)
			.WithMany(p => p.GamePublishers)
			.UsingEntity<GamePublisherPlatform>(
				r => r.HasOne(gpp => gpp.Platform)
					.WithMany(p => p.GamePublisherPlatforms)
					.HasForeignKey(gpp => gpp.PlatformId)
					.OnDelete(DeleteBehavior.NoAction),
				l => l.HasOne(gpp => gpp.GamePublisher)
					.WithMany(gp => gp.GamePublisherPlatforms)
					.HasForeignKey(gpp => new { gpp.GameId, gpp.PublisherId })
					.OnDelete(DeleteBehavior.NoAction)
			);
	}
}
