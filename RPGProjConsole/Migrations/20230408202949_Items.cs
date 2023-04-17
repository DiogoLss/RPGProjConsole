using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGProjConsole.Migrations
{
    public partial class Items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArmaEquipadaId",
                table: "Npcs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArmaduraEquipadaId",
                table: "Npcs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VidaMaxima",
                table: "Npcs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArmaEquipadaId",
                table: "Jogadores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArmaduraEquipadaId",
                table: "Jogadores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VidaMaxima",
                table: "Jogadores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Dinheiro",
                table: "Inventario",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "IncrementoDecisivo",
                table: "Armas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Preco",
                table: "Armas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Armaduras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Peso = table.Column<double>(nullable: false),
                    Preco = table.Column<double>(nullable: false),
                    CA = table.Column<int>(nullable: false),
                    PenalidadeDeDestreza = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armaduras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Peso = table.Column<double>(nullable: false),
                    Preco = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventarioArmaduras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmaduraId = table.Column<int>(nullable: true),
                    InventarioId = table.Column<int>(nullable: true),
                    Qtd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioArmaduras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioArmaduras_Armaduras_ArmaduraId",
                        column: x => x.ArmaduraId,
                        principalTable: "Armaduras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioArmaduras_Inventario_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventarioItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: true),
                    InventarioId = table.Column<int>(nullable: true),
                    Qtd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioItems_Inventario_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Npcs_ArmaEquipadaId",
                table: "Npcs",
                column: "ArmaEquipadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Npcs_ArmaduraEquipadaId",
                table: "Npcs",
                column: "ArmaduraEquipadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_ArmaEquipadaId",
                table: "Jogadores",
                column: "ArmaEquipadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_ArmaduraEquipadaId",
                table: "Jogadores",
                column: "ArmaduraEquipadaId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioArmaduras_ArmaduraId",
                table: "InventarioArmaduras",
                column: "ArmaduraId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioArmaduras_InventarioId",
                table: "InventarioArmaduras",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioItems_InventarioId",
                table: "InventarioItems",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioItems_ItemId",
                table: "InventarioItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Armas_ArmaEquipadaId",
                table: "Jogadores",
                column: "ArmaEquipadaId",
                principalTable: "Armas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Armaduras_ArmaduraEquipadaId",
                table: "Jogadores",
                column: "ArmaduraEquipadaId",
                principalTable: "Armaduras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Npcs_Armas_ArmaEquipadaId",
                table: "Npcs",
                column: "ArmaEquipadaId",
                principalTable: "Armas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Npcs_Armaduras_ArmaduraEquipadaId",
                table: "Npcs",
                column: "ArmaduraEquipadaId",
                principalTable: "Armaduras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Armas_ArmaEquipadaId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Armaduras_ArmaduraEquipadaId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Npcs_Armas_ArmaEquipadaId",
                table: "Npcs");

            migrationBuilder.DropForeignKey(
                name: "FK_Npcs_Armaduras_ArmaduraEquipadaId",
                table: "Npcs");

            migrationBuilder.DropTable(
                name: "InventarioArmaduras");

            migrationBuilder.DropTable(
                name: "InventarioItems");

            migrationBuilder.DropTable(
                name: "Armaduras");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Npcs_ArmaEquipadaId",
                table: "Npcs");

            migrationBuilder.DropIndex(
                name: "IX_Npcs_ArmaduraEquipadaId",
                table: "Npcs");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_ArmaEquipadaId",
                table: "Jogadores");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_ArmaduraEquipadaId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "ArmaEquipadaId",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "ArmaduraEquipadaId",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "VidaMaxima",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "ArmaEquipadaId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "ArmaduraEquipadaId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "VidaMaxima",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Dinheiro",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "IncrementoDecisivo",
                table: "Armas");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Armas");
        }
    }
}
