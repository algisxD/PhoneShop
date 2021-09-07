using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSAPI.Data.Migrations
{
    public partial class new_db_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Darbuotojai",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "TelefonoModeliai",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonoModeliai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Detales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pavadinimas = table.Column<string>(nullable: true),
                    Savikaina = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PagaminimoData = table.Column<DateTime>(nullable: false),
                    KilmesSalis = table.Column<string>(nullable: true),
                    SerijosNumeris = table.Column<string>(nullable: true),
                    TelefonoModelisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detales_TelefonoModeliai_TelefonoModelisId",
                        column: x => x.TelefonoModelisId,
                        principalTable: "TelefonoModeliai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detales_TelefonoModelisId",
                table: "Detales",
                column: "TelefonoModelisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detales");

            migrationBuilder.DropTable(
                name: "TelefonoModeliai");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Darbuotojai",
                newName: "name");
        }
    }
}
