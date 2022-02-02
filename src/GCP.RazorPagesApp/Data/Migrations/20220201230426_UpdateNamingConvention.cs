using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GCP.RazorPagesApp.Data.Migrations
{
    public partial class UpdateNamingConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "game");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "game",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "game",
                newName: "score");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "game",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "GameLink",
                table: "game",
                newName: "game_link");

            migrationBuilder.AddPrimaryKey(
                name: "pk_game",
                table: "game",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_game",
                table: "game");

            migrationBuilder.RenameTable(
                name: "game",
                newName: "Game");

            migrationBuilder.RenameColumn(
                name: "user",
                table: "Game",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "Game",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Game",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "game_link",
                table: "Game",
                newName: "GameLink");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");
        }
    }
}
