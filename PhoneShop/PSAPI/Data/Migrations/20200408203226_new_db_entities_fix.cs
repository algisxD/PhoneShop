using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSAPI.Data.Migrations
{
    public partial class new_db_entities_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gamintojas",
                table: "TelefonoModeliai",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IsleidimoData",
                table: "TelefonoModeliai",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Pavadinimas",
                table: "TelefonoModeliai",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gamintojas",
                table: "TelefonoModeliai");

            migrationBuilder.DropColumn(
                name: "IsleidimoData",
                table: "TelefonoModeliai");

            migrationBuilder.DropColumn(
                name: "Pavadinimas",
                table: "TelefonoModeliai");
        }
    }
}
