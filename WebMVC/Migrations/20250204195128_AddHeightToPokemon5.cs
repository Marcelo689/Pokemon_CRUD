using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddHeightToPokemon5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_PokemonType2_SecondTypeId",
                table: "Pokemon");

            migrationBuilder.DropTable(
                name: "PokemonType2");

            migrationBuilder.CreateTable(
                name: "PokemonSecondType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSecondType", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_PokemonSecondType_SecondTypeId",
                table: "Pokemon",
                column: "SecondTypeId",
                principalTable: "PokemonSecondType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_PokemonSecondType_SecondTypeId",
                table: "Pokemon");

            migrationBuilder.DropTable(
                name: "PokemonSecondType");

            migrationBuilder.CreateTable(
                name: "PokemonType2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonType2", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_PokemonType2_SecondTypeId",
                table: "Pokemon",
                column: "SecondTypeId",
                principalTable: "PokemonType2",
                principalColumn: "Id");
        }
    }
}
