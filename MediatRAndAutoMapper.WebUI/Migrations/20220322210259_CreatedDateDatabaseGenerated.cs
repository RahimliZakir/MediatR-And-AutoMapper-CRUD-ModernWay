using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatRAndAutoMapper.WebUI.Migrations
{
    public partial class CreatedDateDatabaseGenerated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Passengers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Passengers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(HOUR, 4, GETUTCDATE())");
        }
    }
}
