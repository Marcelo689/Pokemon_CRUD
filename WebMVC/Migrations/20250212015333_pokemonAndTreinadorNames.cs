using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class pokemonAndTreinadorNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PokemonTreinador",
                newName: "TreinadorName");

            migrationBuilder.AddColumn<string>(
                name: "PokemonName",
                table: "PokemonTreinador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PokemonName",
                table: "PokemonTreinador");

            migrationBuilder.RenameColumn(
                name: "TreinadorName",
                table: "PokemonTreinador",
                newName: "Name");
        }
    }
}
