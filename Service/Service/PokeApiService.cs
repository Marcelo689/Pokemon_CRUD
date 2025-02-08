using ApiModelsResponse.ApiModels;
using ApiModelsResponse.ApiModels.ApiResponse;
using ApiModelsResponse.ViewModels;
using DB.Data;
using DB.Models;
using Service.Service;
using System.Text.Json;
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

        public PokemonDetails GetPokemonDetailsByName(string pokemonName)
        {
            return _context.PokemonStatsDetails.First(e => e.Name.Contains(pokemonName.ToLower()));
        }

        public PokemonDetailsViewModel AddClassesToPokemonDetailsViewModel(PokemonDetailsViewModel pokemonDetailsViewModel)
        {
            pokemonDetailsViewModel.Ability1 = C.Convert(_context.PokemonAbility.First(e => e.Id == pokemonDetailsViewModel.Ability1Id));
            if (pokemonDetailsViewModel.Ability2Id != 0)
                pokemonDetailsViewModel.Ability2 = C.Convert(_context.PokemonAbility.First(e => e.Id == pokemonDetailsViewModel.Ability2Id));
            if(pokemonDetailsViewModel.Ability3Id != 0)
                pokemonDetailsViewModel.Ability3 = C.Convert(_context.PokemonAbility.First(e => e.Id == pokemonDetailsViewModel.Ability3Id));

            pokemonDetailsViewModel.PokemonType1 = C.Convert(_context.PokemonType.First(e => e.Id == pokemonDetailsViewModel.PokemonTypeId1));

            if (pokemonDetailsViewModel.PokemonTypeId2 is not null)
                pokemonDetailsViewModel.PokemonType2 = C.Convert(_context.PokemonType.First(e => e.Id == pokemonDetailsViewModel.PokemonTypeId2));

            return pokemonDetailsViewModel;
        }

        public AbilityDetailsViewModel GetPokemonAbilityByName(string abilityName)
        {
            AbilityDetailsViewModel viewModel = new AbilityDetailsViewModel();
            var ability = _context.PokemonAbility.First(e => e.Name == abilityName);
            var pokemonsWithSameAbilty = _context.PokemonStatsDetails.Where(e =>
                e.Ability1Id == ability.Id ||
                e.Ability2Id == ability.Id ||
                e.Ability3Id == ability.Id
            );

            viewModel.Pokemon = new();
            foreach (var pokemon in pokemonsWithSameAbilty)
            {
                viewModel.Pokemon.Add(pokemon.Name);
            }

            viewModel.Name = ability.Name;
            viewModel.EffectEntries = ability.Description;
            viewModel.Description = ability.Description;
            return viewModel;
        }
    }

}
