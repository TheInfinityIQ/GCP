using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GCP.Api.Data.Migrations
{
	/// <inheritdoc />
	public partial class OwnedGameEntity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "owned_game",
				columns: table => new
				{
					game_id = table.Column<int>(type: "integer", nullable: false),
					user_id = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_owned_game", x => new { x.user_id, x.game_id });
					table.ForeignKey(
						name: "fk_owned_game_asp_net_users_user_id",
						column: x => x.user_id,
						principalTable: "user",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_owned_game_game_game_id",
						column: x => x.game_id,
						principalTable: "game",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "ix_owned_game_game_id",
				table: "owned_game",
				column: "game_id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "owned_game");
		}
	}
}
