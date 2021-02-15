using Microsoft.EntityFrameworkCore.Migrations;

namespace sudokuBackEnd.Migrations
{
    public partial class addAvarageTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvarageTime",
                table: "GameEntering",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GameEntering",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSuccessfullSolutions",
                table: "GameEntering",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvarageTime",
                table: "GameEntering");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GameEntering");

            migrationBuilder.DropColumn(
                name: "NumberOfSuccessfullSolutions",
                table: "GameEntering");
        }
    }
}
