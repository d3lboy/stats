using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stats.Api.Migrations
{
    public partial class ProperForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Season",
                table: "RoundDto",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Season",
                table: "RoundDto",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
