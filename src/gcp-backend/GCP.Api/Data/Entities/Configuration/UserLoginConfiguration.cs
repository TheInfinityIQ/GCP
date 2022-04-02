using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
	public void Configure(EntityTypeBuilder<UserLogin> builder)
	{
		builder.ToTableSnakeCase(nameof(UserLogin));
	}
}
