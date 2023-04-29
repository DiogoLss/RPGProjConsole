﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPGProjConsole.context;

namespace RPGProjConsole.Migrations
{
    [DbContext(typeof(RPGContext))]
    [Migration("20230427172457_arvoreAdded")]
    partial class arvoreAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RPGProjConsole.models.Arma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Dados")
                        .HasColumnType("int");

                    b.Property<int>("DadosDOT")
                        .HasColumnType("int");

                    b.Property<bool>("DamageOverTime")
                        .HasColumnType("bit");

                    b.Property<string>("Dano")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Durabilidade")
                        .HasColumnType("int");

                    b.Property<int>("IncrementoDecisivo")
                        .HasColumnType("int");

                    b.Property<bool>("IsCorpoACorpo")
                        .HasColumnType("bit");

                    b.Property<int>("MunicaoMaxima")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.Property<string>("TipoDeDano")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ValorDado")
                        .HasColumnType("int");

                    b.Property<int>("ValorDadoDOT")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Armas");
                });

            modelBuilder.Entity("RPGProjConsole.models.Armadura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CA")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PenalidadeDeDestreza")
                        .HasColumnType("int");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Armaduras");
                });

            modelBuilder.Entity("RPGProjConsole.models.ArvoreDeHabilidades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdicionalMaximo")
                        .HasColumnType("int");

                    b.Property<int>("Arvore")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("NecessarioTer")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ArvoreHabilidades");
                });

            modelBuilder.Entity("RPGProjConsole.models.Inventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Dinheiro")
                        .HasColumnType("float");

                    b.Property<double>("PesoTotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("RPGProjConsole.models.InventarioArma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArmaId")
                        .HasColumnType("int");

                    b.Property<int?>("InventarioId")
                        .HasColumnType("int");

                    b.Property<int>("MunicaoCarregada")
                        .HasColumnType("int");

                    b.Property<int>("MunicaoReserva")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArmaId");

                    b.HasIndex("InventarioId");

                    b.ToTable("InventarioArmas");
                });

            modelBuilder.Entity("RPGProjConsole.models.InventarioArmadura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArmaduraId")
                        .HasColumnType("int");

                    b.Property<int?>("InventarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArmaduraId");

                    b.HasIndex("InventarioId");

                    b.ToTable("InventarioArmaduras");
                });

            modelBuilder.Entity("RPGProjConsole.models.InventarioItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("InventarioId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Qtd")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventarioId");

                    b.HasIndex("ItemId");

                    b.ToTable("InventarioItems");
                });

            modelBuilder.Entity("RPGProjConsole.models.ItemGeneral", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ECura")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.Property<int>("QtdDadosCura")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("RPGProjConsole.models.Jogador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArmaEquipadaId")
                        .HasColumnType("int");

                    b.Property<int?>("ArmaduraEquipadaId")
                        .HasColumnType("int");

                    b.Property<int>("AtaqueBase")
                        .HasColumnType("int");

                    b.Property<int>("CA")
                        .HasColumnType("int");

                    b.Property<int>("Carisma")
                        .HasColumnType("int");

                    b.Property<int>("Constituicao")
                        .HasColumnType("int");

                    b.Property<int>("Destreza")
                        .HasColumnType("int");

                    b.Property<int>("Exp")
                        .HasColumnType("int");

                    b.Property<int>("Forca")
                        .HasColumnType("int");

                    b.Property<int>("Inteligencia")
                        .HasColumnType("int");

                    b.Property<int?>("InventarioId")
                        .HasColumnType("int");

                    b.Property<int>("Medo")
                        .HasColumnType("int");

                    b.Property<int>("MunicaoArmaEquipada")
                        .HasColumnType("int");

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PericiaMaxima")
                        .HasColumnType("int");

                    b.Property<int>("Sabedoria")
                        .HasColumnType("int");

                    b.Property<int>("VidaMaxima")
                        .HasColumnType("int");

                    b.Property<int>("Vitalidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArmaEquipadaId");

                    b.HasIndex("ArmaduraEquipadaId");

                    b.HasIndex("InventarioId");

                    b.ToTable("Jogadores");
                });

            modelBuilder.Entity("RPGProjConsole.models.JogadorArvore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArvoreDeHabilidadesId")
                        .HasColumnType("int");

                    b.Property<int>("EscolhaId")
                        .HasColumnType("int");

                    b.Property<int?>("JogadorId")
                        .HasColumnType("int");

                    b.Property<int>("QtdAdicional")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArvoreDeHabilidadesId");

                    b.HasIndex("JogadorId");

                    b.ToTable("JogadorArvores");
                });

            modelBuilder.Entity("RPGProjConsole.models.JogadorPericia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("JogadorId")
                        .HasColumnType("int");

                    b.Property<int?>("PericiaId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JogadorId");

                    b.HasIndex("PericiaId");

                    b.ToTable("JogadorPericia");
                });

            modelBuilder.Entity("RPGProjConsole.models.Npcs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArmaEquipadaId")
                        .HasColumnType("int");

                    b.Property<int?>("ArmaduraEquipadaId")
                        .HasColumnType("int");

                    b.Property<int>("AtaqueBase")
                        .HasColumnType("int");

                    b.Property<int>("CA")
                        .HasColumnType("int");

                    b.Property<int>("Carisma")
                        .HasColumnType("int");

                    b.Property<int>("Constituicao")
                        .HasColumnType("int");

                    b.Property<int>("Destreza")
                        .HasColumnType("int");

                    b.Property<int>("Exp")
                        .HasColumnType("int");

                    b.Property<int>("Forca")
                        .HasColumnType("int");

                    b.Property<int>("Inteligencia")
                        .HasColumnType("int");

                    b.Property<int?>("InventarioId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMob")
                        .HasColumnType("bit");

                    b.Property<int>("Medo")
                        .HasColumnType("int");

                    b.Property<int>("MunicaoArmaEquipada")
                        .HasColumnType("int");

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PericiaMaxima")
                        .HasColumnType("int");

                    b.Property<int>("Sabedoria")
                        .HasColumnType("int");

                    b.Property<int>("VidaMaxima")
                        .HasColumnType("int");

                    b.Property<int>("Vitalidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArmaEquipadaId");

                    b.HasIndex("ArmaduraEquipadaId");

                    b.HasIndex("InventarioId");

                    b.ToTable("Npcs");
                });

            modelBuilder.Entity("RPGProjConsole.models.Pericia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IndexHabilidade")
                        .HasColumnType("int");

                    b.Property<bool>("SemTreinamento")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Pericias");
                });

            modelBuilder.Entity("RPGProjConsole.models.InventarioArma", b =>
                {
                    b.HasOne("RPGProjConsole.models.Arma", "Arma")
                        .WithMany()
                        .HasForeignKey("ArmaId");

                    b.HasOne("RPGProjConsole.models.Inventario", "Inventario")
                        .WithMany("InventarioArmas")
                        .HasForeignKey("InventarioId");
                });

            modelBuilder.Entity("RPGProjConsole.models.InventarioArmadura", b =>
                {
                    b.HasOne("RPGProjConsole.models.Armadura", "Armadura")
                        .WithMany()
                        .HasForeignKey("ArmaduraId");

                    b.HasOne("RPGProjConsole.models.Inventario", "Inventario")
                        .WithMany("InventarioArmaduras")
                        .HasForeignKey("InventarioId");
                });

            modelBuilder.Entity("RPGProjConsole.models.InventarioItem", b =>
                {
                    b.HasOne("RPGProjConsole.models.Inventario", "Inventario")
                        .WithMany("InventarioItems")
                        .HasForeignKey("InventarioId");

                    b.HasOne("RPGProjConsole.models.ItemGeneral", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("RPGProjConsole.models.Jogador", b =>
                {
                    b.HasOne("RPGProjConsole.models.Arma", "ArmaEquipada")
                        .WithMany()
                        .HasForeignKey("ArmaEquipadaId");

                    b.HasOne("RPGProjConsole.models.Armadura", "ArmaduraEquipada")
                        .WithMany()
                        .HasForeignKey("ArmaduraEquipadaId");

                    b.HasOne("RPGProjConsole.models.Inventario", "Inventario")
                        .WithMany()
                        .HasForeignKey("InventarioId");
                });

            modelBuilder.Entity("RPGProjConsole.models.JogadorArvore", b =>
                {
                    b.HasOne("RPGProjConsole.models.ArvoreDeHabilidades", "ArvoreDeHabilidades")
                        .WithMany()
                        .HasForeignKey("ArvoreDeHabilidadesId");

                    b.HasOne("RPGProjConsole.models.Jogador", "Jogador")
                        .WithMany()
                        .HasForeignKey("JogadorId");
                });

            modelBuilder.Entity("RPGProjConsole.models.JogadorPericia", b =>
                {
                    b.HasOne("RPGProjConsole.models.Jogador", "Jogador")
                        .WithMany()
                        .HasForeignKey("JogadorId");

                    b.HasOne("RPGProjConsole.models.Pericia", "Pericia")
                        .WithMany()
                        .HasForeignKey("PericiaId");
                });

            modelBuilder.Entity("RPGProjConsole.models.Npcs", b =>
                {
                    b.HasOne("RPGProjConsole.models.Arma", "ArmaEquipada")
                        .WithMany()
                        .HasForeignKey("ArmaEquipadaId");

                    b.HasOne("RPGProjConsole.models.Armadura", "ArmaduraEquipada")
                        .WithMany()
                        .HasForeignKey("ArmaduraEquipadaId");

                    b.HasOne("RPGProjConsole.models.Inventario", "Inventario")
                        .WithMany()
                        .HasForeignKey("InventarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
