using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGProjConsole.Migrations
{
    public partial class arvores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PericiaMaxima",
                table: "Npcs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PericiaMaxima",
                table: "Jogadores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DadosDOT",
                table: "Armas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DamageOverTime",
                table: "Armas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ValorDadoDOT",
                table: "Armas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArvoreHabilidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Arvore = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AdicionalMaximo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArvoreHabilidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JogadorArvores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JogadorId = table.Column<int>(nullable: true),
                    ArvoreDeHabilidadesId = table.Column<int>(nullable: true),
                    QtdAdicional = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogadorArvores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JogadorArvores_ArvoreHabilidades_ArvoreDeHabilidadesId",
                        column: x => x.ArvoreDeHabilidadesId,
                        principalTable: "ArvoreHabilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JogadorArvores_Jogadores_JogadorId",
                        column: x => x.JogadorId,
                        principalTable: "Jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JogadorArvores_ArvoreDeHabilidadesId",
                table: "JogadorArvores",
                column: "ArvoreDeHabilidadesId");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorArvores_JogadorId",
                table: "JogadorArvores",
                column: "JogadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JogadorArvores");

            migrationBuilder.DropTable(
                name: "ArvoreHabilidades");

            migrationBuilder.DropColumn(
                name: "PericiaMaxima",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "PericiaMaxima",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "DadosDOT",
                table: "Armas");

            migrationBuilder.DropColumn(
                name: "DamageOverTime",
                table: "Armas");

            migrationBuilder.DropColumn(
                name: "ValorDadoDOT",
                table: "Armas");
        }
    }
}
