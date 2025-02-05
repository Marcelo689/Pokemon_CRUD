using Aspnet_core_web_api.Entities;
using Microsoft.Data.SqlClient;
using Dapper;
namespace WebMVC.EndPoint
{
    public static class PokemonEndPoints
    {
        public static void MapProductEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("pokemons", async (IConfiguration configuration) =>
            {
                const string query = "SELECT * FROM pokemon";
                string connectionString = configuration.GetConnectionString("DbConnectionString");

                using SqlConnection connection = new(connectionString);

                var pokemons = await connection.QueryAsync<Pokemon>(query);

                return Results.Ok(pokemons);    
            });
        }
    }
}
