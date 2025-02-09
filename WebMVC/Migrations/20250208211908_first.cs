using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: true),
                    isPhysical = table.Column<bool>(type: "bit", nullable: false),
                    isSpecial = table.Column<bool>(type: "bit", nullable: false),
                    isStatus = table.Column<bool>(type: "bit", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: true),
                    PP = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonMove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoveId = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: true),
                    Accuracy = table.Column<int>(type: "int", nullable: true),
                    PP = table.Column<int>(type: "int", nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMove", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonStatsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ability1Id = table.Column<int>(type: "int", nullable: false),
                    Ability2Id = table.Column<int>(type: "int", nullable: false),
                    Ability3Id = table.Column<int>(type: "int", nullable: false),
                    PokemonTypeId1 = table.Column<int>(type: "int", nullable: true),
                    PokemonTypeId2 = table.Column<int>(type: "int", nullable: true),
                    HP = table.Column<int>(type: "int", nullable: false),
                    ATTACK = table.Column<int>(type: "int", nullable: false),
                    DEFENSE = table.Column<int>(type: "int", nullable: false),
                    SP_ATTACK = table.Column<int>(type: "int", nullable: false),
                    SP_DEFENSE = table.Column<int>(type: "int", nullable: false),
                    SPEED = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weight = table.Column<int>(type: "int", nullable: false),
                    height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStatsDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treinador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTreinador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreinadorId = table.Column<int>(type: "int", nullable: false),
                    PokemonApiIdId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    SecondTypeId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Move1Id = table.Column<int>(type: "int", nullable: false),
                    Move2Id = table.Column<int>(type: "int", nullable: false),
                    Move3Id = table.Column<int>(type: "int", nullable: false),
                    Move4Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTreinador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonTreinador_PokemonAbility_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "PokemonAbility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonTreinador_PokemonType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PokemonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonTreinador_Pokemon_PokemonApiIdId",
                        column: x => x.PokemonApiIdId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonTreinador_Treinador_TreinadorId",
                        column: x => x.TreinadorId,
                        principalTable: "Treinador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTreinador_AbilityId",
                table: "PokemonTreinador",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTreinador_PokemonApiIdId",
                table: "PokemonTreinador",
                column: "PokemonApiIdId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTreinador_TreinadorId",
                table: "PokemonTreinador",
                column: "TreinadorId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTreinador_TypeId",
                table: "PokemonTreinador",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "PokemonMove");

            migrationBuilder.DropTable(
                name: "PokemonStatsDetails");

            migrationBuilder.DropTable(
                name: "PokemonTreinador");

            migrationBuilder.DropTable(
                name: "PokemonAbility");

            migrationBuilder.DropTable(
                name: "PokemonType");

            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "Treinador");
        }
    }
}
