using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddHeightToPokemon2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_PokemonType_SecondTypeId",
                table: "Pokemon");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemon_PokemonType2_SecondTypeId",
                table: "Pokemon");

            migrationBuilder.DropTable(
                name: "PokemonType2");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemon_PokemonType_SecondTypeId",
                table: "Pokemon",
                column: "SecondTypeId",
                principalTable: "PokemonType",
                principalColumn: "Id");
        }
    }
}
