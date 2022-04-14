using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GCP.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class EarlyGameListEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "game_list",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    owner_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    normalized_title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "character varying(550)", maxLength: 550, nullable: true),
                    vote_once_per_game = table.Column<bool>(type: "boolean", nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    user_limit = table.Column<int>(type: "integer", nullable: true),
                    created_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_list", x => x.id);
                    table.ForeignKey(
                        name: "fk_game_list_asp_net_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_list_user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    game_list_id = table.Column<int>(type: "integer", nullable: false),
                    joined_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_list_user", x => new { x.game_list_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_game_list_user_game_list_game_list_id",
                        column: x => x.game_list_id,
                        principalTable: "game_list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_game_list_user_users_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_game_list_description",
                table: "game_list",
                column: "description");

            migrationBuilder.CreateIndex(
                name: "ix_game_list_last_updated_on_utc",
                table: "game_list",
                column: "last_updated_on_utc");

            migrationBuilder.CreateIndex(
                name: "ix_game_list_normalized_title",
                table: "game_list",
                column: "normalized_title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_game_list_owner_id",
                table: "game_list",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_game_list_title",
                table: "game_list",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_game_list_user_user_id",
                table: "game_list_user",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_list_user");

            migrationBuilder.DropTable(
                name: "game_list");
        }
    }
}
