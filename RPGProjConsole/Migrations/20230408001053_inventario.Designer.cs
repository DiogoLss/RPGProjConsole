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
    [Migration("20230408001053_inventario")]
    partial class inventario
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

                    b.Property<string>("Dano")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorpoACorpo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.Property<string>("TipoDeDano")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ValorDado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Armas");
                });

            modelBuilder.Entity("RPGProjConsole.models.Inventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int>("Qtd")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArmaId");

                    b.HasIndex("InventarioId");

                    b.ToTable("InventarioArmas");
                });

            modelBuilder.Entity("RPGProjConsole.models.Jogador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sabedoria")
                        .HasColumnType("int");

                    b.Property<int>("Vitalidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventarioId");

                    b.ToTable("Jogadores");
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

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sabedoria")
                        .HasColumnType("int");

                    b.Property<int>("Vitalidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

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

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

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

            modelBuilder.Entity("RPGProjConsole.models.Jogador", b =>
                {
                    b.HasOne("RPGProjConsole.models.Inventario", "Inventario")
                        .WithMany()
                        .HasForeignKey("InventarioId");
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
                    b.HasOne("RPGProjConsole.models.Inventario", "Inventario")
                        .WithMany()
                        .HasForeignKey("InventarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
