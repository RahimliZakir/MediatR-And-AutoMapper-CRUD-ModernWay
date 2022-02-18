using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatRAndAutoMapper.WebUI.Migrations
{
    public partial class CreatedByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_CreatedByUserId",
                table: "Passengers",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Users_CreatedByUserId",
                table: "Passengers",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Users_CreatedByUserId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_CreatedByUserId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Passengers");
        }
    }
}
