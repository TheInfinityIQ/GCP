
using GCP.RazorPagesApp.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.RazorPagesApp.Data.Entities.Configuration;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
	public void Configure(EntityTypeBuilder<UserClaim> builder)
	{
		builder.ToTableSnakeCase(nameof(UserClaim));
	}
}
