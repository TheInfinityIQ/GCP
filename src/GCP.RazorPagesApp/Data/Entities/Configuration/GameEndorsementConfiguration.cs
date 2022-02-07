
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class GameEndorsementConfiguration : IEntityTypeConfiguration<GameEndorsement>
{
	public void Configure(EntityTypeBuilder<GameEndorsement> builder)
	{
		builder.HasKey(ge => ge.Id);

		builder.UseXminAsConcurrencyToken();

		builder.Property(ge => ge.Weight)
			.IsRequired();
	}
}
