using System.Text.Json;
using WebMVC.ApiModels;
using WebMVC.ApiModels.ApiResponse;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Service
{
    public class ViewModel
    {
        public List<PokemonViewModelResponse> PokemonList { get; set; } = new List<PokemonViewModelResponse>();
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class PokeApiService
    {
        private readonly HttpClient _httpClient;

        public readonly PokemonContext _context;

        public PokeApiService(HttpClient httpClient, PokemonContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<ViewModel> ConsultAllPokemonAsync()
        {
            int limit = 1000;
            string url = $"https://pokeapi.co/api/v2/pokemon?limit={limit}";
            var response = await _httpClient.GetStringAsync(url);
            var pokemonList = JsonSerializer.Deserialize<PokemonListResponse>(response);

            var pokemons = new List<PokemonViewModelResponse>();

            foreach (var pokemon in pokemonList.Results)
            {
                var detail = await GetPokemonDetailAsync(pokemon.Url);
                pokemons.Add(new PokemonViewModelResponse
                {
                    Name = pokemon.Name,
                    Url = detail.Sprites.Other.OfficialArtwork.FrontDefault
                });
            }

            return new ViewModel { 
                 PokemonList = pokemons
            };
        }

        private async Task<PokemonDetailSprites> GetPokemonDetailAsync(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<PokemonDetailSprites>(response);
        }

        public List<Pokemon> GetAllPokemonApi()
        {
            return _context.Pokemon.ToList();
        }

        public async Task SavePokemonAsync(Pokemon pokemon)
        {
            _context.Pokemon.Add(pokemon);
        }

        public async Task SavePokemonDetailsAsync(PokemonDetails details)
        {
            _context.PokemonStatsDetails.Add(details);
        }
    }

}
