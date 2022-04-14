using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public const int DISPLAY_NAME_MAX_LENGTH = 30;

	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTableSnakeCase(nameof(User));

		builder.Property(u => u.DisplayName)
			.HasMaxLength(DISPLAY_NAME_MAX_LENGTH)
			.IsUnicode();

		builder.HasIndex(u => u.DisplayName)
			.IsUnique();

		builder.HasMany(u => u.OwnedGames)
			.WithMany(g => g.Owners)
			.UsingEntity<OwnedGame>();
	}
}
