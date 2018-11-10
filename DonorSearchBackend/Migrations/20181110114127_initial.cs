using Microsoft.EntityFrameworkCore.Migrations;

namespace DonorSearchBackend.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blood_type_id",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "blood_type",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blood_type",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "blood_type_id",
                table: "Users",
                nullable: true);
        }
    }
}
