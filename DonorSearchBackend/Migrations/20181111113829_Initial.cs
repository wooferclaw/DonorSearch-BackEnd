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
                    city_title = table.Column<string>(nullable: true),
                    region_title = table.Column<string>(nullable: true),
                    about_self = table.Column<string>(nullable: true),
                    blood_type = table.Column<string>(nullable: true),
                    blood_class_ids = table.Column<int>(nullable: false),
                    bone_marrow = table.Column<bool>(nullable: true),
                    cant_to_be_donor = table.Column<bool>(nullable: true),
                    donor_pause_to = table.Column<DateTime>(nullable: true),
                    has_registration = table.Column<bool>(nullable: true),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    is_first_donor = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.vk_id);
                });

            migrationBuilder.CreateTable(
                name: "ValidationVisit",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date_from = table.Column<DateTime>(nullable: true),
                    date_to = table.Column<DateTime>(nullable: true),
                    visit_date = table.Column<DateTime>(nullable: true),
                    success = table.Column<bool>(nullable: true),
                    without_donation = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationVisit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ds_Id = table.Column<int>(nullable: true),
                    vk_id = table.Column<int>(nullable: false),
                    appointment_date_from = table.Column<DateTime>(nullable: false),
                    appointment_date_to = table.Column<DateTime>(nullable: false),
                    donation_date = table.Column<DateTime>(nullable: true),
                    donation_success = table.Column<bool>(nullable: true),
                    blood_class_ids = table.Column<int>(nullable: false),
                    Img = table.Column<string>(type: "text", nullable: true),
                    station_id = table.Column<int>(nullable: true),
                    station_title = table.Column<string>(nullable: true),
                    station_address = table.Column<string>(nullable: true),
                    recomendation_timestamp = table.Column<DateTime>(nullable: true),
                    finished = table.Column<bool>(nullable: false),
                    confirm_visitid = table.Column<int>(nullable: true),
                    previous_donation_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Donations_ValidationVisit_confirm_visitid",
                        column: x => x.confirm_visitid,
                        principalTable: "ValidationVisit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_confirm_visitid",
                table: "Donations",
                column: "confirm_visitid");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_id",
                table: "Donations",
                column: "id",
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

            migrationBuilder.DropTable(
                name: "ValidationVisit");
        }
    }
}
