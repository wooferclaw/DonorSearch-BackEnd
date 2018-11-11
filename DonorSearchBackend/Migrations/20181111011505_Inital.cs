using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorSearchBackend.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "station_address",
                table: "Donations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "station_title",
                table: "Donations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "station_address",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "station_title",
                table: "Donations");
        }
    }
}
