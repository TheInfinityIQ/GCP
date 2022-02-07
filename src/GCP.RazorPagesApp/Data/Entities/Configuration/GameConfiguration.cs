
using GCP.RazorPagesApp.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
	public void Configure(EntityTypeBuilder<Game> builder)
	{
		builder.HasKey(g => g.Id);

		builder.UseXminAsConcurrencyToken();

		builder.Property(g => g.Title)
			.IsRequired()
			.IsUnicode();

		builder.Property(g => g.NormalizedTitle)
			.IsRequired()
			.IsUnicode();
		builder.HasIndex(g => g.NormalizedTitle)
			.IsCreatedConcurrently()
			.IsUnique();

		builder.Property(g => g.ReleaseDate)
			.HasColumnType("date")
			.HasConversion<DateOnlyValueConverter>();

		builder.Property(g => g.MaxPlayers)
			.IsRequired();

		builder.HasMany(g => g.Publishers)
			.WithMany(p => p.Games)
			.UsingEntity<GamePublisher>(
				r => r.HasOne(gp => gp.Publisher)
					.WithMany(g => g.GamePublishers)
					.HasForeignKey(gp => gp.PublisherId)
					.OnDelete(DeleteBehavior.NoAction),
				l => l.HasOne(gp => gp.Game)
					.WithMany(g => g.GamePublishers)
					.HasForeignKey(gp => gp.GameId)
					.OnDelete(DeleteBehavior.NoAction)
			);

		builder.HasMany(g => g.Genres)
			.WithMany(g => g.Games)
			.UsingEntity<GameGenre>(
				r => r.HasOne(gg => gg.Genre)
					.WithMany(g => g.GameGenres)
					.HasForeignKey(gg => gg.GenreId)
					.OnDelete(DeleteBehavior.NoAction),
				l => l.HasOne(gg => gg.Game)
					.WithMany(g => g.GameGenres)
					.HasForeignKey(gg => gg.GameId)
					.OnDelete(DeleteBehavior.NoAction)
			);

		builder.HasMany(g => g.Endorsements)
			.WithOne(ge => ge.Game)
			.HasForeignKey(ge => ge.GameId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(g => g.Retailers)
			.WithMany(r => r.Games)
			.UsingEntity<GameRetailer>(
				r => r.HasOne(gp => gp.Retailer)
					.WithMany(g => g.GameRetailers)
					.HasForeignKey(gp => gp.RetailerId)
					.OnDelete(DeleteBehavior.NoAction),
				l => l.HasOne(gp => gp.Game)
					.WithMany(g => g.GameRetailers)
					.HasForeignKey(gp => gp.GameId)
					.OnDelete(DeleteBehavior.NoAction)
			);

		builder.HasMany(g => g.GameLinks)
			.WithOne(gl => gl.Game)
			.HasForeignKey(gl => gl.GameId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Cascade);
	}
}
