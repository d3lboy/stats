using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stats.Api.Migrations
{
    public partial class Models3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Players",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<short>(
                name: "Height",
                table: "Players",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BoxScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: true),
                    GameId = table.Column<Guid>(nullable: true),
                    SecondsPlayed = table.Column<int>(nullable: false),
                    Fg1M = table.Column<int>(nullable: false),
                    Fg1A = table.Column<int>(nullable: false),
                    Fg2M = table.Column<int>(nullable: false),
                    Fg2A = table.Column<int>(nullable: false),
                    Fg3M = table.Column<int>(nullable: false),
                    Fg3A = table.Column<int>(nullable: false),
                    RebsO = table.Column<int>(nullable: false),
                    RebsD = table.Column<int>(nullable: false),
                    Ass = table.Column<int>(nullable: false),
                    St = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false),
                    BlckFv = table.Column<int>(nullable: false),
                    BlckAg = table.Column<int>(nullable: false),
                    FoulCm = table.Column<int>(nullable: false),
                    FoulRv = table.Column<int>(nullable: false),
                    Val = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxScores_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxScores_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxScores_GameId",
                table: "BoxScores",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxScores_PlayerId",
                table: "BoxScores",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxScores");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Players");
        }
    }
}
