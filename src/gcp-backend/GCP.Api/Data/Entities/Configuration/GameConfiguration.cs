using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;
public class GameConfiguration : IEntityTypeConfiguration<Game>
{
	public void Configure(EntityTypeBuilder<Game> builder)
	{
		builder.Property(g => g.Name)
			.IsRequired()
			.IsUnicode();
		builder.Property(g => g.NormalizedName)
			.IsRequired()
			.IsUnicode();

		builder.Property(g => g.Metadata).HasColumnType("jsonb");

		builder.HasIndex(g => g.Metadata);
		builder.HasIndex(g => g.Name).IsUnique();
		builder.HasIndex(g => g.NormalizedName).IsUnique();
		builder.HasIndex(g => g.SteamAppId).IsUnique();
	}
}
