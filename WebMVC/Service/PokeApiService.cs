using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebMVC.ApiModels;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Service
{
    public class ViewModel
    {
        public List<PokemonViewModel> PokemonList { get; set; } = new List<PokemonViewModel>();
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

        public async Task<ViewModel> GetAllPokemonAsync()
        {
            int limit = 1000;
            string url = $"https://pokeapi.co/api/v2/pokemon?limit={limit}";
            var response = await _httpClient.GetStringAsync(url);
            var pokemonList = JsonSerializer.Deserialize<PokemonListResponse>(response);

            var pokemons = new List<PokemonViewModel>();

            foreach (var pokemon in pokemonList.Results)
            {
                var detail = await GetPokemonDetailAsync(pokemon.Url);
                pokemons.Add(new PokemonViewModel
                {
                    Name = pokemon.Name,
                    ImageUrl = detail.Sprites.Other.OfficialArtwork.FrontDefault
                });
            }

            return new ViewModel { 
                 PokemonList = pokemons
            };
        }

        private async Task<PokemonDetail> GetPokemonDetailAsync(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<PokemonDetail>(response);
        }

        public List<PokemonApi> GetAllPokemonApi()
        {
            return _context.PokemonApi.ToList();
        }

        public async Task SavePokemonAsync(PokemonApi pokemon)
        {
            _context.PokemonApi.Add(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task SavePokemonDetailsAsync(PokemonApiDetails details)
        {
            _context.PokemonApiDetails.Add(details);
            await _context.SaveChangesAsync();
        }

        public bool AlreadyHasPokemonDetails(int id)
        {
            return _context.PokemonApiDetails.Where(e => e.PokemonId != id).Any();
        }
    }

}
