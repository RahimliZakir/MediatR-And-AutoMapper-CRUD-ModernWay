using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatRAndAutoMapper.WebUI.Migrations
{
    public partial class GeneratedSecretKeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeneratedSecretKey",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneratedSecretKey",
                table: "Passengers");
        }
    }
}
