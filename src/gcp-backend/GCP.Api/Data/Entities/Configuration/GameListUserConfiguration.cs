using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;

public class GameListUserConfiguration : IEntityTypeConfiguration<GameListUser>
{
	public void Configure(EntityTypeBuilder<GameListUser> builder)
	{
		builder.ToTableSnakeCase(nameof(GameListUser));
		builder.HasKey(z => new { z.GameListId, z.UserId });
		builder.HasOne(z => z.GameList)
			.WithMany()
			.HasForeignKey(z => z.GameListId);
		builder.HasOne(z => z.User)
			.WithMany()
			.HasForeignKey(z => z.UserId);
	}
}
