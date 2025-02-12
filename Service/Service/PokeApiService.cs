using ApiModelsResponse.ApiModels;
using ApiModelsResponse.ApiModels.ApiResponse;
using ApiModelsResponse.ViewModels;
using DB.Data;
using DB.Models;
using ModelsResponse.ViewModels;
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

        public List<MoveViewModel> GetMovesFromPokemon(string pokemonName)
        {
            return _context.PokemonMove.Where(e => e.PokemonName.Equals(pokemonName)).Select( e => new MoveViewModel()
            {
                Id = e.MoveId,
                Name = e.MoveName,
                PokemonName = e.PokemonName,
                Power = e.Power,
                Accuracy = e.Accuracy,
                IsSpecial = e.IsSpecial,
                IsPhysical = e.isPhysical,
                IsStatus = e.isStatus,
                PP = e.PP,
                ShortDescription = e.ShortDescription,
            }).ToList();
        }

        public MoveDetailsViewModel GetMoveFromName(string moveName)
        {
            var move = _context.Move.FirstOrDefault(e => e.Name == moveName);

            if (move is null)
                return null;
            return new MoveDetailsViewModel
            {
                Id = move.Id,
                Name = move.Name,
                Power = move.Power,
                Accuracy = move.Accuracy,
                IsSpecial = move.isSpecial,
                IsPhysical = move.isPhysical,
                IsStatus = move.isStatus,
                Description = move.Description,
                ShorDescription = move.ShortDescription,
                PP = move.PP,
            };
        }
    }

}
