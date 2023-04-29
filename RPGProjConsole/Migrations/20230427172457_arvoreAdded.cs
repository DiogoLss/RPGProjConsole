using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGProjConsole.Migrations
{
    public partial class arvoreAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EscolhaId",
                table: "JogadorArvores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ECura",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QtdDadosCura",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NecessarioTer",
                table: "ArvoreHabilidades",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EscolhaId",
                table: "JogadorArvores");

            migrationBuilder.DropColumn(
                name: "ECura",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "QtdDadosCura",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "NecessarioTer",
                table: "ArvoreHabilidades");
        }
    }
}
