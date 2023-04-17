using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGProjConsole.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogadores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Vitalidade = table.Column<int>(nullable: false),
                    Medo = table.Column<int>(nullable: false),
                    AtaqueBase = table.Column<int>(nullable: false),
                    CA = table.Column<int>(nullable: false),
                    Exp = table.Column<int>(nullable: false),
                    Forca = table.Column<int>(nullable: false),
                    Destreza = table.Column<int>(nullable: false),
                    Constituicao = table.Column<int>(nullable: false),
                    Inteligencia = table.Column<int>(nullable: false),
                    Sabedoria = table.Column<int>(nullable: false),
                    Carisma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Npcs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Vitalidade = table.Column<int>(nullable: false),
                    AtaqueBase = table.Column<int>(nullable: false),
                    CA = table.Column<int>(nullable: false),
                    Forca = table.Column<int>(nullable: false),
                    Destreza = table.Column<int>(nullable: false),
                    Constituicao = table.Column<int>(nullable: false),
                    Inteligencia = table.Column<int>(nullable: false),
                    Sabedoria = table.Column<int>(nullable: false),
                    Carisma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Npcs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pericias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true),
                    IndexHabilidade = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pericias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JogadorPericia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JogadorId = table.Column<int>(nullable: true),
                    PericiaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogadorPericia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JogadorPericia_Jogadores_JogadorId",
                        column: x => x.JogadorId,
                        principalTable: "Jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JogadorPericia_Pericias_PericiaId",
                        column: x => x.PericiaId,
                        principalTable: "Pericias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JogadorPericia_JogadorId",
                table: "JogadorPericia",
                column: "JogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorPericia_PericiaId",
                table: "JogadorPericia",
                column: "PericiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JogadorPericia");

            migrationBuilder.DropTable(
                name: "Npcs");

            migrationBuilder.DropTable(
                name: "Jogadores");

            migrationBuilder.DropTable(
                name: "Pericias");
        }
    }
}
