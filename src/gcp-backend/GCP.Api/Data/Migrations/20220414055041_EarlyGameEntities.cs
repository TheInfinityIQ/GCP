using GCP.Api.Data.Entities;

using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GCP.Api.Data.Migrations
{
	/// <inheritdoc />
	public partial class EarlyGameEntities : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "game",
				columns: table => new
				{
					id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					name = table.Column<string>(type: "text", nullable: false),
					normalized_name = table.Column<string>(type: "text", nullable: false),
					metadata = table.Column<GameMetadata>(type: "jsonb", nullable: false),
					steam_app_id = table.Column<long>(type: "bigint", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_game", x => x.id);
				});

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
						name: "fk_owned_game_game_game_id",
						column: x => x.game_id,
						principalTable: "game",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_owned_game_users_user_id",
						column: x => x.user_id,
						principalTable: "user",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "ix_game_metadata",
				table: "game",
				column: "metadata");

			migrationBuilder.CreateIndex(
				name: "ix_game_name",
				table: "game",
				column: "name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "ix_game_normalized_name",
				table: "game",
				column: "normalized_name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "ix_game_steam_app_id",
				table: "game",
				column: "steam_app_id",
				unique: true);

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

			migrationBuilder.DropTable(
				name: "game");
		}
	}
}
