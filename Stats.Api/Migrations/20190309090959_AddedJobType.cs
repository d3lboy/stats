using Microsoft.EntityFrameworkCore.Migrations;

namespace Stats.Api.Migrations
{
    public partial class AddedJobType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "RoundDto",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "RoundDto");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Jobs");
        }
    }
}
