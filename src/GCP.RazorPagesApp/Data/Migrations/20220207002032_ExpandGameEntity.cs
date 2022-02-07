using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GCP.RazorPagesApp.Data.Migrations
{
    public partial class ExpandGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "game_link",
                table: "game");

            migrationBuilder.DropColumn(
                name: "user",
                table: "game");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "game",
                newName: "max_players");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:link_type", "unknown,article,store,image,video");

            migrationBuilder.AddColumn<string>(
                name: "normalized_title",
                table: "game",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "release_date",
                table: "game",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "supports_online_play",
                table: "game",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "game",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "game",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateTable(
                name: "game_endorsement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    game_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_endorsement", x => x.id);
                    table.ForeignKey(
                        name: "fk_game_endorsement_game_game_id",
                        column: x => x.game_id,
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_game_endorsement_users_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_link",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    game_id = table.Column<int>(type: "integer", nullable: false),
                    label = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_link", x => x.id);
                    table.ForeignKey(
                        name: "fk_game_link_game_game_id",
                        column: x => x.game_id,
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    normalized_name = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "platform",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    normalized_name = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_platform", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publisher",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    normalized_name = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publisher", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "retailer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    normalized_name = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_retailer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_genre",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "integer", nullable: false),
                    genre_id = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_genre", x => new { x.game_id, x.genre_id });
                    table.ForeignKey(
                        name: "fk_game_genre_game_game_id",
                        column: x => x.game_id,
                        principalTable: "game",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_game_genre_genre_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genre",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "game_publisher",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "integer", nullable: false),
                    publisher_id = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_publisher", x => new { x.game_id, x.publisher_id });
                    table.ForeignKey(
                        name: "fk_game_publisher_game_game_id",
                        column: x => x.game_id,
                        principalTable: "game",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_game_publisher_publisher_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publisher",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "game_retailer",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "integer", nullable: false),
                    retailer_id = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_retailer", x => new { x.game_id, x.retailer_id });
                    table.ForeignKey(
                        name: "fk_game_retailer_game_game_id",
                        column: x => x.game_id,
                        principalTable: "game",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_game_retailer_retailer_retailer_id",
                        column: x => x.retailer_id,
                        principalTable: "retailer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "game_publisher_platform",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "integer", nullable: false),
                    publisher_id = table.Column<int>(type: "integer", nullable: false),
                    platform_id = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_publisher_platform", x => new { x.game_id, x.publisher_id, x.platform_id });
                    table.ForeignKey(
                        name: "fk_game_publisher_platform_game_game_id",
                        column: x => x.game_id,
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_game_publisher_platform_game_publisher_game_publisher_temp_",
                        columns: x => new { x.game_id, x.publisher_id },
                        principalTable: "game_publisher",
                        principalColumns: new[] { "game_id", "publisher_id" });
                    table.ForeignKey(
                        name: "fk_game_publisher_platform_platform_platform_id",
                        column: x => x.platform_id,
                        principalTable: "platform",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_game_normalized_title",
                table: "game",
                column: "normalized_title",
                unique: true)
                .Annotation("Npgsql:CreatedConcurrently", true);

            migrationBuilder.CreateIndex(
                name: "ix_game_endorsement_game_id",
                table: "game_endorsement",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_game_endorsement_user_id",
                table: "game_endorsement",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_game_genre_genre_id",
                table: "game_genre",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "ix_game_link_game_id",
                table: "game_link",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_game_link_link",
                table: "game_link",
                column: "link",
                unique: true)
                .Annotation("Npgsql:CreatedConcurrently", true);

            migrationBuilder.CreateIndex(
                name: "ix_game_publisher_publisher_id",
                table: "game_publisher",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_game_publisher_platform_platform_id",
                table: "game_publisher_platform",
                column: "platform_id");

            migrationBuilder.CreateIndex(
                name: "ix_game_retailer_retailer_id",
                table: "game_retailer",
                column: "retailer_id");

            migrationBuilder.CreateIndex(
                name: "ix_genre_normalized_name",
                table: "genre",
                column: "normalized_name",
                unique: true)
                .Annotation("Npgsql:CreatedConcurrently", true);

            migrationBuilder.CreateIndex(
                name: "ix_platform_normalized_name",
                table: "platform",
                column: "normalized_name",
                unique: true)
                .Annotation("Npgsql:CreatedConcurrently", true);

            migrationBuilder.CreateIndex(
                name: "ix_publisher_normalized_name",
                table: "publisher",
                column: "normalized_name",
                unique: true)
                .Annotation("Npgsql:CreatedConcurrently", true);

            migrationBuilder.CreateIndex(
                name: "ix_retailer_normalized_name",
                table: "retailer",
                column: "normalized_name",
                unique: true)
                .Annotation("Npgsql:CreatedConcurrently", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_endorsement");

            migrationBuilder.DropTable(
                name: "game_genre");

            migrationBuilder.DropTable(
                name: "game_link");

            migrationBuilder.DropTable(
                name: "game_publisher_platform");

            migrationBuilder.DropTable(
                name: "game_retailer");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "game_publisher");

            migrationBuilder.DropTable(
                name: "platform");

            migrationBuilder.DropTable(
                name: "retailer");

            migrationBuilder.DropTable(
                name: "publisher");

            migrationBuilder.DropIndex(
                name: "ix_game_normalized_title",
                table: "game");

            migrationBuilder.DropColumn(
                name: "normalized_title",
                table: "game");

            migrationBuilder.DropColumn(
                name: "release_date",
                table: "game");

            migrationBuilder.DropColumn(
                name: "supports_online_play",
                table: "game");

            migrationBuilder.DropColumn(
                name: "title",
                table: "game");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "game");

            migrationBuilder.RenameColumn(
                name: "max_players",
                table: "game",
                newName: "score");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:link_type", "unknown,article,store,image,video");

            migrationBuilder.AddColumn<string>(
                name: "game_link",
                table: "game",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user",
                table: "game",
                type: "text",
                nullable: true);
        }
    }
}
