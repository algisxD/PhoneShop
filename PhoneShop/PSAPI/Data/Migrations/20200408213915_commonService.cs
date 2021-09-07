using Microsoft.EntityFrameworkCore.Migrations;

namespace PSAPI.Data.Migrations
{
    public partial class commonService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detales_TelefonoModeliai_TelefonoModelisId",
                table: "Detales");

            migrationBuilder.AlterColumn<int>(
                name: "TelefonoModelisId",
                table: "Detales",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Detales_TelefonoModeliai_TelefonoModelisId",
                table: "Detales",
                column: "TelefonoModelisId",
                principalTable: "TelefonoModeliai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detales_TelefonoModeliai_TelefonoModelisId",
                table: "Detales");

            migrationBuilder.AlterColumn<int>(
                name: "TelefonoModelisId",
                table: "Detales",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Detales_TelefonoModeliai_TelefonoModelisId",
                table: "Detales",
                column: "TelefonoModelisId",
                principalTable: "TelefonoModeliai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
