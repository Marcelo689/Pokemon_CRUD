using PokemonConsoleWorld;
using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

static async Task<Pokemon> ProcessRepositoriesAsync(HttpClient client, string link)
{
    var json = await client.GetStringAsync(link);
    Pokemon pokemon = JsonSerializer.Deserialize<Pokemon>(json);

    return pokemon;
}

var baseLink = "https://pokeapi.co/api/v2/";
string pokemonSearch = "pokemon/charizard";

string searchLink = baseLink + pokemonSearch;

 Pokemon data = await ProcessRepositoriesAsync(client, searchLink);

string algo = "adada";

Console.WriteLine(data.Name);

Console.WriteLine("\nHabilidades\n");

foreach (var item in data.Abilities)
{
    Console.WriteLine(item.Abilities.Name);
}

Console.WriteLine("\nItens segurados\n");

foreach (var item in data.HeldItems) {
    Console.WriteLine(item.Items.Name);
}

Console.WriteLine("\nMovimentos\n");

foreach (var item in data.Moves)
{
    Console.WriteLine(item.Moves.Name);
}


