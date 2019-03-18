using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stats.Api.Migrations
{
    public partial class CreatedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Seasons",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Rounds",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Periods",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Competitions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "BoxScores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Periods");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BoxScores");
        }
    }
}
