
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class GameLinkConfiguration : IEntityTypeConfiguration<GameLink>
{
	public void Configure(EntityTypeBuilder<GameLink> builder)
	{
		builder.HasKey(ge => ge.Id);

		builder.UseXminAsConcurrencyToken();

		builder.Property(g => g.Label)
			.IsUnicode();

		builder.Property(g => g.Type)
			.IsRequired();

		builder.Property(g => g.Link)
			.IsRequired()
			.IsUnicode();
		builder.HasIndex(g => g.Link)
			.IsCreatedConcurrently()
			.IsUnique();
	}
}
