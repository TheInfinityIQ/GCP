using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Data.Entities.Configuration;
public class OwnedGameConfiguration : IEntityTypeConfiguration<OwnedGame>
{
	public void Configure(EntityTypeBuilder<OwnedGame> builder)
	{
		builder.ToTableSnakeCase(nameof(OwnedGame));
		builder.HasKey(og => new { og.UserId, og.GameId });
		builder.HasOne(og => og.Game)
			.WithMany()
			.HasForeignKey(og => og.GameId);
		builder.HasOne(og => og.User)
			.WithMany()
			.HasForeignKey(og => og.UserId);
	}
}
