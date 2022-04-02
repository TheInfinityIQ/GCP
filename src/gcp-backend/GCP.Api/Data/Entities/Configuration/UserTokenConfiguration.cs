using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
	public void Configure(EntityTypeBuilder<UserToken> builder)
	{
		builder.ToTableSnakeCase(nameof(UserToken));
	}
}
