using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;

public class GameListUserConfiguration : IEntityTypeConfiguration<GameListUser>
{
	public void Configure(EntityTypeBuilder<GameListUser> builder)
	{
		builder.ToTableSnakeCase(nameof(GameListUser));
		builder.HasKey(glu => new { glu.GameListId, glu.UserId });
		builder.HasOne(glu => glu.GameList)
			.WithMany()
			.HasForeignKey(glu => glu.GameListId);
		builder.HasOne(glu => glu.User)
			.WithMany()
			.HasForeignKey(glu => glu.UserId);
	}
}
