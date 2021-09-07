using Microsoft.EntityFrameworkCore.Migrations;

namespace PSAPI.Data.Migrations
{
    public partial class Uzsakymo_ir_saskaitos_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KlientoId",
                table: "Uzsakymai",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TelefonoModelisId",
                table: "Uzsakymai",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UzsakymasId",
                table: "Saskaitos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uzsakymai_TelefonoModelisId",
                table: "Uzsakymai",
                column: "TelefonoModelisId");

            migrationBuilder.CreateIndex(
                name: "IX_Saskaitos_UzsakymasId",
                table: "Saskaitos",
                column: "UzsakymasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saskaitos_Uzsakymai_UzsakymasId",
                table: "Saskaitos",
                column: "UzsakymasId",
                principalTable: "Uzsakymai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Uzsakymai_TelefonoModeliai_TelefonoModelisId",
                table: "Uzsakymai",
                column: "TelefonoModelisId",
                principalTable: "TelefonoModeliai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saskaitos_Uzsakymai_UzsakymasId",
                table: "Saskaitos");

            migrationBuilder.DropForeignKey(
                name: "FK_Uzsakymai_TelefonoModeliai_TelefonoModelisId",
                table: "Uzsakymai");

            migrationBuilder.DropIndex(
                name: "IX_Uzsakymai_TelefonoModelisId",
                table: "Uzsakymai");

            migrationBuilder.DropIndex(
                name: "IX_Saskaitos_UzsakymasId",
                table: "Saskaitos");

            migrationBuilder.DropColumn(
                name: "KlientoId",
                table: "Uzsakymai");

            migrationBuilder.DropColumn(
                name: "TelefonoModelisId",
                table: "Uzsakymai");

            migrationBuilder.DropColumn(
                name: "UzsakymasId",
                table: "Saskaitos");
        }
    }
}
