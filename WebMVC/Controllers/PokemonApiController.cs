using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebMVC.ApiModels;
using WebMVC.Models;
using WebMVC.Service;

namespace WebMVC.Controllers
{
    public class PokemonApiController : Controller
    {
        private readonly PokeApiService _pokeApiService;
        private ViewModel viewModel;
        public PokemonApiController(PokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string search = "")
        {
            var pokemonApiList = _pokeApiService.GetAllPokemonApi();
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = 1000;
            ViewBag.SearchQuery = search;

            if (pokemonApiList.Any())
            {
                pokemonApiList = pokemonApiList.Where(e => e.Name.ToLower().Contains(search.ToLower())).ToList();
                viewModel = new ViewModel();
                viewModel.PokemonList = pokemonApiList.Select(e => new PokemonViewModel
                {
                    Name = e.Name,
                    ImageUrl = e.Url
                }).ToList();
            }
            else
            {
                viewModel = await _pokeApiService.ConsultAllPokemonAsync();
                await SavingApiDataIntoDB(viewModel);
            }

            if (!string.IsNullOrEmpty(search))
            {
                pokemonApiList
                .Where(p => p.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            }
            var totalCount = pokemonApiList.Count;

            // Paginando os resultados filtrados
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            ViewBag.TotalPages = totalPages;
            var pagedResults = viewModel.PokemonList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList(); 

            return View(pagedResults);
        }

        private async Task SavingApiDataIntoDB(ViewModel viewModel)
        {
            foreach (var pokemon in viewModel.PokemonList)
            {
                var pokemonEntity = new PokemonApi
                {
                    Name = pokemon.Name,
                    Url = pokemon.ImageUrl
                };

                _pokeApiService.SavePokemonAsync(pokemonEntity);
                //var detailsEntity = await GetDetailsPokemonFromApiSaveInDB(pokemon.Name, null);
                //_pokeApiService.SavePokemonDetailsAsync(detailsEntity);
            }

            _pokeApiService.SaveChanges();
        }

        public async Task<ActionResult> AbilityDetails(string abilityName)
        {
            if (string.IsNullOrEmpty(abilityName))
            {
                return RedirectToAction("Index");
            }

            // A URL to fetch detailed information about the ability from the API
            string url = $"https://pokeapi.co/api/v2/ability/{abilityName.ToLower()}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var abilityDetails = JsonSerializer.Deserialize<AbilityDetailsResponse>(response);

                // Pass the ability details to the view
                return View(abilityDetails);
            }
        }

        public async Task<ActionResult> Details(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                return RedirectToAction("Index");
            }

            PokemonDetailsResponse pokemonDetails = null;
            string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                pokemonDetails = JsonSerializer.Deserialize<PokemonDetailsResponse>(response);
            }

            return View(pokemonDetails);
        }

        private async Task<PokemonApiDetails> GetDetailsPokemonFromApiSaveInDB(string pokemonName, PokemonDetailsResponse pokemonDetails)
        {
            PokemonApiDetails details = null;   
            string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                pokemonDetails = JsonSerializer.Deserialize<PokemonDetailsResponse>(response);
                // Passar as informações para a View
                details = await AddObjectDetailsOnDB(pokemonDetails);
            }

            return details;
        }

        private async Task<PokemonApiDetails> AddObjectDetailsOnDB(PokemonDetailsResponse? pokemonDetails)
        {
            PokemonApiDetails detailsEntity = null;
            if (!_pokeApiService.AlreadyHasPokemonDetails(pokemonDetails.Id))
            {
                detailsEntity = new PokemonApiDetails
                {
                    PokemonId = pokemonDetails.Id, // Assume you have a relationship between Pokemon and PokemonDetails
                    Type = string.Join(", ", pokemonDetails.Types.Select(t => t.Type.Name)),
                    Abilities = string.Join(", ", pokemonDetails.Abilities.Select(a => a.Ability.Name)),
                    Height = pokemonDetails.height,
                    Weight = pokemonDetails.weight,
                    Stats = new PokemonApiStat
                    {
                        Stat_HP = pokemonDetails.Stats[0].BaseStat,
                        Stat_ATTACK = pokemonDetails.Stats[1].BaseStat,
                        Stat_DEFENSE = pokemonDetails.Stats[2].BaseStat,
                        Stat_SP_ATTACK = pokemonDetails.Stats[3].BaseStat,
                        Stat__SP_DEFENSE = pokemonDetails.Stats[4].BaseStat,
                        Stat_SPEED = pokemonDetails.Stats[5].BaseStat,
                    }
                };

                await _pokeApiService.SavePokemonDetailsAsync(detailsEntity);
            }
            return detailsEntity;
        }
    }
}
