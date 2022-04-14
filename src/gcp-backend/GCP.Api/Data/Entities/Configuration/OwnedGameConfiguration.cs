using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;
public class OwnedGameConfiguration : IEntityTypeConfiguration<OwnedGame>
{
	public void Configure(EntityTypeBuilder<OwnedGame> builder)
	{
		builder.ToTableSnakeCase(nameof(OwnedGame));
		builder.HasKey(z => new { z.UserId, z.GameId });
		builder.HasOne(z => z.Game)
			.WithMany()
			.HasForeignKey(z => z.GameId);
		builder.HasOne(z => z.User)
			.WithMany()
			.HasForeignKey(z => z.UserId);
	}
}
