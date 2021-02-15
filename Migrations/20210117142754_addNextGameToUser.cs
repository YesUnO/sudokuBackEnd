using Microsoft.EntityFrameworkCore.Migrations;

namespace sudokuBackEnd.Migrations
{
    public partial class addNextGameToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextgameEnteringToSolveId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_NextgameEnteringToSolveId",
                table: "User",
                column: "NextgameEnteringToSolveId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_GameEntering_NextgameEnteringToSolveId",
                table: "User",
                column: "NextgameEnteringToSolveId",
                principalTable: "GameEntering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_GameEntering_NextgameEnteringToSolveId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_NextgameEnteringToSolveId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NextgameEnteringToSolveId",
                table: "User");
        }
    }
}
