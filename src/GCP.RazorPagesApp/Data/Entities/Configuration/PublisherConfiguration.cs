
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
	public void Configure(EntityTypeBuilder<Publisher> builder)
	{
		builder.HasKey(p => p.Id);

		builder.UseXminAsConcurrencyToken();

		builder.Property(p => p.Name)
			.IsRequired()
			.IsUnicode();

		builder.Property(p => p.NormalizedName)
			.IsRequired()
			.IsUnicode();
		builder.HasIndex(p => p.NormalizedName)
			.IsCreatedConcurrently()
			.IsUnique();
	}
}
