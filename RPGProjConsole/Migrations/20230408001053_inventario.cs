using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGProjConsole.Migrations
{
    public partial class inventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Exp",
                table: "Npcs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventarioId",
                table: "Npcs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMob",
                table: "Npcs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Medo",
                table: "Npcs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Nivel",
                table: "Npcs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventarioId",
                table: "Jogadores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nivel",
                table: "Jogadores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Armas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Peso = table.Column<double>(nullable: false),
                    TipoDeDano = table.Column<string>(nullable: true),
                    Dano = table.Column<string>(nullable: true),
                    Dados = table.Column<int>(nullable: false),
                    ValorDado = table.Column<int>(nullable: false),
                    IsCorpoACorpo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PesoTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventarioArmas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmaId = table.Column<int>(nullable: true),
                    InventarioId = table.Column<int>(nullable: true),
                    Qtd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioArmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioArmas_Armas_ArmaId",
                        column: x => x.ArmaId,
                        principalTable: "Armas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioArmas_Inventario_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Npcs_InventarioId",
                table: "Npcs",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_InventarioId",
                table: "Jogadores",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioArmas_ArmaId",
                table: "InventarioArmas",
                column: "ArmaId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioArmas_InventarioId",
                table: "InventarioArmas",
                column: "InventarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Inventario_InventarioId",
                table: "Jogadores",
                column: "InventarioId",
                principalTable: "Inventario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Npcs_Inventario_InventarioId",
                table: "Npcs",
                column: "InventarioId",
                principalTable: "Inventario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Inventario_InventarioId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Npcs_Inventario_InventarioId",
                table: "Npcs");

            migrationBuilder.DropTable(
                name: "InventarioArmas");

            migrationBuilder.DropTable(
                name: "Armas");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Npcs_InventarioId",
                table: "Npcs");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_InventarioId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Exp",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "InventarioId",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "IsMob",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "Medo",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "InventarioId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "Jogadores");
        }
    }
}
