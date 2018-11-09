using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DonorSearchBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VkId = table.Column<int>(nullable: false),
                    DonationTimestamp = table.Column<DateTime>(nullable: false),
                    StationId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    DonationType = table.Column<int>(nullable: false),
                    DonationId = table.Column<int>(nullable: false),
                    Succeed = table.Column<bool>(nullable: false),
                    RecomendationTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    VkId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DSId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.VkId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_Id",
                table: "Donations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DSId",
                table: "Users",
                column: "DSId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_VkId",
                table: "Users",
                column: "VkId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
