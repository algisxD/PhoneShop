using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSAPI.Data.Migrations
{
    public partial class saskaita_ir_uzsakymas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Saskaitos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Suma = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApmokejimoData = table.Column<DateTime>(nullable: true),
                    ApmokejimoTerminas = table.Column<DateTime>(nullable: false),
                    Busena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saskaitos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzsakymai",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzsakymoTipas = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    ApmokejimoBusena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzsakymai", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Saskaitos");

            migrationBuilder.DropTable(
                name: "Uzsakymai");
        }
    }
}
