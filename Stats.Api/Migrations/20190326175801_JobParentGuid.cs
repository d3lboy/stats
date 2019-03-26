using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stats.Api.Migrations
{
    public partial class JobParentGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Parent",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parent",
                table: "Jobs");
        }
    }
}
