using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGProjConsole.Migrations
{
    public partial class Fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qtd",
                table: "InventarioArmas");

            migrationBuilder.DropColumn(
                name: "Qtd",
                table: "InventarioArmaduras");

            migrationBuilder.DropColumn(
                name: "MunicaoCarregada",
                table: "Armas");

            migrationBuilder.DropColumn(
                name: "MunicaoPermitida",
                table: "Armas");

            migrationBuilder.AddColumn<int>(
                name: "MunicaoArmaEquipada",
                table: "Npcs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicaoArmaEquipada",
                table: "Jogadores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicaoCarregada",
                table: "InventarioArmas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicaoMaxima",
                table: "Armas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MunicaoArmaEquipada",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "MunicaoArmaEquipada",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "MunicaoCarregada",
                table: "InventarioArmas");

            migrationBuilder.DropColumn(
                name: "MunicaoMaxima",
                table: "Armas");

            migrationBuilder.AddColumn<int>(
                name: "Qtd",
                table: "InventarioArmas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qtd",
                table: "InventarioArmaduras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicaoCarregada",
                table: "Armas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicaoPermitida",
                table: "Armas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
