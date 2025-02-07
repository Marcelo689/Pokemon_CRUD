using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebMVC.ApiModels.ApiResponse;
using WebMVC.Models;
using WebMVC.Service;
using WebMVC.ViewModels;

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

            if (!string.IsNullOrEmpty(search))
            {
                pokemonApiList = pokemonApiList
                .Where(p => p.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            }
            var totalCount = pokemonApiList.Count;

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            ViewBag.TotalPages = totalPages;
            var pagedResults = pokemonApiList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select( e => new PokemonViewModel { 
                    Name = e.Name,
                    Url = e.Url
                })
                .ToList(); 

            return View(pagedResults);
        }

        private async Task SavingApiDataIntoDB(ViewModel viewModel)
        {
            foreach (var pokemon in viewModel.PokemonList)
            {
                var pokemonEntity = new Pokemon
                {
                    Name = pokemon.Name,
                    Url = pokemon.Url
                };
            }
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
    }
}
