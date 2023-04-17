using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGProjConsole.Migrations
{
    public partial class arma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Pericias");

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "JogadorPericia",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicaoReserva",
                table: "InventarioArmas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Durabilidade",
                table: "Armas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicaoCarregada",
                table: "Armas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "JogadorPericia");

            migrationBuilder.DropColumn(
                name: "MunicaoReserva",
                table: "InventarioArmas");

            migrationBuilder.DropColumn(
                name: "Durabilidade",
                table: "Armas");

            migrationBuilder.DropColumn(
                name: "MunicaoCarregada",
                table: "Armas");

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Pericias",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
