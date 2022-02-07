
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
	public void Configure(EntityTypeBuilder<Genre> builder)
	{
		builder.HasKey(g => g.Id);

		builder.UseXminAsConcurrencyToken();

		builder.Property(g => g.Name)
			.IsRequired()
			.IsUnicode();

		builder.Property(g => g.NormalizedName)
			.IsRequired()
			.IsUnicode();
		builder.HasIndex(g => g.NormalizedName)
			.IsCreatedConcurrently()
			.IsUnique();
	}
}
