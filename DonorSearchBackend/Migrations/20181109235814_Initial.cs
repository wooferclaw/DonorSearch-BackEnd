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
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    blood_class_ids = table.Column<int>(nullable: false),
                    donation_Id = table.Column<int>(nullable: true),
                    succeed = table.Column<bool>(nullable: true),
                    recomendation_timestamp = table.Column<DateTime>(nullable: false),
                    vk_id = table.Column<int>(nullable: false),
                    donation_timestamp = table.Column<DateTime>(nullable: false),
                    station_id = table.Column<int>(nullable: false),
                    status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    vk_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    second_name = table.Column<string>(nullable: true),
                    maiden_name = table.Column<string>(nullable: true),
                    bdate = table.Column<DateTime>(nullable: true),
                    gender = table.Column<int>(nullable: true),
                    city_id = table.Column<int>(nullable: true),
                    about_self = table.Column<string>(nullable: true),
                    blood_type_id = table.Column<int>(nullable: true),
                    blood_class_ids = table.Column<int>(nullable: false),
                    bone_marrow = table.Column<bool>(nullable: true),
                    cant_to_be_donor = table.Column<bool>(nullable: true),
                    donor_pause_to = table.Column<int>(nullable: true),
                    has_registration = table.Column<bool>(nullable: true),
                    ds_id = table.Column<int>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.vk_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_id",
                table: "Donations",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ds_id",
                table: "Users",
                column: "ds_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_vk_id",
                table: "Users",
                column: "vk_id",
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
