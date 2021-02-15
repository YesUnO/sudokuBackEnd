using Microsoft.EntityFrameworkCore.Migrations;

namespace sudokuBackEnd.Migrations
{
    public partial class removeNextGameFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameEntering_User_UserId",
                table: "GameEntering");

            migrationBuilder.DropForeignKey(
                name: "FK_User_GameEntering_NextgameEnteringToSolveId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_NextgameEnteringToSolveId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_GameEntering_UserId",
                table: "GameEntering");

            migrationBuilder.DropColumn(
                name: "NextgameEnteringToSolveId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameEntering");

            migrationBuilder.AddColumn<int>(
                name: "ResolvedGamesCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResolvedGamesCount",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "NextgameEnteringToSolveId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GameEntering",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_NextgameEnteringToSolveId",
                table: "User",
                column: "NextgameEnteringToSolveId");

            migrationBuilder.CreateIndex(
                name: "IX_GameEntering_UserId",
                table: "GameEntering",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameEntering_User_UserId",
                table: "GameEntering",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_GameEntering_NextgameEnteringToSolveId",
                table: "User",
                column: "NextgameEnteringToSolveId",
                principalTable: "GameEntering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
