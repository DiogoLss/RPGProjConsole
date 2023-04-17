using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGProjConsole.Migrations
{
    public partial class ST : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SemTreinamento",
                table: "Pericias",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SemTreinamento",
                table: "Pericias");
        }
    }
}
