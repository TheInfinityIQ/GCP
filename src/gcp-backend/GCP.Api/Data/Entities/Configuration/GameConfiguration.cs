using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;
public class GameConfiguration : IEntityTypeConfiguration<Game>
{
	public void Configure(EntityTypeBuilder<Game> builder)
	{
		builder.Property(x => x.Name)
			.IsRequired()
			.IsUnicode();
		builder.Property(x => x.NormalizedName)
			.IsRequired()
			.IsUnicode();

		builder.Property(x => x.Metadata).HasColumnType("jsonb");

		builder.HasIndex(x => x.Metadata);
		builder.HasIndex(x => x.Name).IsUnique();
		builder.HasIndex(x => x.NormalizedName).IsUnique();
		builder.HasIndex(x => x.SteamAppId).IsUnique();
	}
}
