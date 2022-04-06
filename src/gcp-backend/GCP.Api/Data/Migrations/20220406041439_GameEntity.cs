using GCP.Api.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GCP.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class GameEntity : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game");
        }
    }
}
