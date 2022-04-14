using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;
public class GameListConfiguration : IEntityTypeConfiguration<GameList>
{
	public const int TITLE_MAX_LENGTH = 30;
	public const int DESCRIPTION_MAX_LENGTH = 550;

	public void Configure(EntityTypeBuilder<GameList> builder)
	{
		builder.ToTableSnakeCase(nameof(GameList));
		builder.Property(gl => gl.Title)
			.IsUnicode()
			.IsRequired()
			.HasMaxLength(TITLE_MAX_LENGTH);

		builder.Property(gl => gl.NormalizedTitle)
			.IsUnicode()
			.IsRequired()
			.HasMaxLength(TITLE_MAX_LENGTH);

		builder.Property(gl => gl.Description)
			.IsUnicode()
			.HasMaxLength(DESCRIPTION_MAX_LENGTH);

		builder.HasIndex(gl => gl.Title).IsUnique();
		builder.HasIndex(gl => gl.NormalizedTitle).IsUnique();
		builder.HasIndex(gl => gl.Description);
		builder.HasIndex(gl => gl.LastUpdatedOnUtc);

		builder.HasOne(gl => gl.Owner)
			.WithMany()
			.IsRequired()
			.HasForeignKey(gl => gl.OwnerId);

		builder.HasOne(gl => gl.Owner)
			.WithMany(u => u.OwnedGameLists)
			.HasForeignKey(gl => gl.OwnerId)
			.IsRequired();

		builder.HasMany(gl => gl.Users)
			.WithMany(u => u.JoinedGameLists)
			.UsingEntity<GameListUser>();
	}
}
