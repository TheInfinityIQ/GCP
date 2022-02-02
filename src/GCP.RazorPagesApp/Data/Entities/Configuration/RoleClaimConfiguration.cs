
using GCP.RazorPagesApp.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
	public void Configure(EntityTypeBuilder<RoleClaim> builder)
	{
		builder.ToTableSnakeCase(nameof(RoleClaim));
	}
}
