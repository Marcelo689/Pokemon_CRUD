﻿// <auto-generated />
using DB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebMVC.Migrations
{
    [DbContext(typeof(PokemonContext))]
    [Migration("20250212015333_pokemonAndTreinadorNames")]
    partial class pokemonAndTreinadorNames
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DB.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Accuracy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PP")
                        .HasColumnType("int");

                    b.Property<int?>("Power")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isPhysical")
                        .HasColumnType("bit");

                    b.Property<bool>("isSpecial")
                        .HasColumnType("bit");

                    b.Property<bool>("isStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Move");
                });

            modelBuilder.Entity("DB.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("DB.Models.PokemonAbility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemonAbility");
                });

            modelBuilder.Entity("DB.Models.PokemonDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ATTACK")
                        .HasColumnType("int");

                    b.Property<int>("Ability1Id")
                        .HasColumnType("int");

                    b.Property<int>("Ability2Id")
                        .HasColumnType("int");

                    b.Property<int>("Ability3Id")
                        .HasColumnType("int");

                    b.Property<int>("DEFENSE")
                        .HasColumnType("int");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int?>("PokemonTypeId1")
                        .HasColumnType("int");

                    b.Property<int?>("PokemonTypeId2")
                        .HasColumnType("int");

                    b.Property<int>("SPEED")
                        .HasColumnType("int");

                    b.Property<int>("SP_ATTACK")
                        .HasColumnType("int");

                    b.Property<int>("SP_DEFENSE")
                        .HasColumnType("int");

                    b.Property<int>("height")
                        .HasColumnType("int");

                    b.Property<int>("weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PokemonStatsDetails");
                });

            modelBuilder.Entity("DB.Models.PokemonMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Accuracy")
                        .HasColumnType("int");

                    b.Property<bool>("IsSpecial")
                        .HasColumnType("bit");

                    b.Property<int>("MoveId")
                        .HasColumnType("int");

                    b.Property<string>("MoveName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PP")
                        .HasColumnType("int");

                    b.Property<string>("PokemonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Power")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemonMove");
                });

            modelBuilder.Entity("DB.Models.PokemonTreinadorRelacionado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AbilityId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("Move1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Move2Id")
                        .HasColumnType("int");

                    b.Property<int?>("Move3Id")
                        .HasColumnType("int");

                    b.Property<int?>("Move4Id")
                        .HasColumnType("int");

                    b.Property<int>("PokemonApiIdId")
                        .HasColumnType("int");

                    b.Property<int>("PokemonIndex")
                        .HasColumnType("int");

                    b.Property<string>("PokemonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PokemonNature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecondTypeId")
                        .HasColumnType("int");

                    b.Property<int>("TreinadorId")
                        .HasColumnType("int");

                    b.Property<string>("TreinadorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AbilityId");

                    b.HasIndex("PokemonApiIdId");

                    b.HasIndex("TreinadorId");

                    b.HasIndex("TypeId");

                    b.ToTable("PokemonTreinador");
                });

            modelBuilder.Entity("DB.Models.PokemonType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemonType");
                });

            modelBuilder.Entity("DB.Models.Treinador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Treinador");
                });

            modelBuilder.Entity("DB.Models.PokemonTreinadorRelacionado", b =>
                {
                    b.HasOne("DB.Models.PokemonAbility", "Ability")
                        .WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Models.Pokemon", "PokemonApiId")
                        .WithMany("Pokemons")
                        .HasForeignKey("PokemonApiIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Models.Treinador", "Treinador")
                        .WithMany("Pokemons")
                        .HasForeignKey("TreinadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DB.Models.PokemonType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ability");

                    b.Navigation("PokemonApiId");

                    b.Navigation("Treinador");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("DB.Models.Pokemon", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("DB.Models.Treinador", b =>
                {
                    b.Navigation("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}
