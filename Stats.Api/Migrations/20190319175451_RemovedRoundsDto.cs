using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stats.Api.Migrations
{
    public partial class RemovedRoundsDto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoundDto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoundDto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoundNumber = table.Column<int>(nullable: false),
                    RoundType = table.Column<int>(nullable: false),
                    Season = table.Column<Guid>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundDto", x => x.Id);
                });
        }
    }
}
