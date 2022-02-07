
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
{
	public void Configure(EntityTypeBuilder<GameGenre> builder)
	{
		builder.HasKey(gg => new { gg.GameId, gg.GenreId });

		builder.UseXminAsConcurrencyToken();
	}
}
