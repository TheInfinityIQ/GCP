
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class RetailerConfiguration : IEntityTypeConfiguration<Retailer>
{
	public void Configure(EntityTypeBuilder<Retailer> builder)
	{
		builder.HasKey(ge => ge.Id);

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
