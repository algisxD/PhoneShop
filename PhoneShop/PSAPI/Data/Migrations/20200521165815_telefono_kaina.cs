using Microsoft.EntityFrameworkCore.Migrations;

namespace PSAPI.Data.Migrations
{
    public partial class telefono_kaina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Kaina",
                table: "TelefonoModeliai",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kaina",
                table: "TelefonoModeliai");
        }
    }
}
