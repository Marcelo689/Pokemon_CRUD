using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult> AbilityDetails(string abilityName)
        {
            if (string.IsNullOrEmpty(abilityName))
            {
                return RedirectToAction("Index");
            }
            AbilityDetailsViewModel abilityDetails = _pokeApiService.GetPokemonAbilityByName(abilityName);

            return View(abilityDetails);
        }

        public async Task<ActionResult> Details(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                return RedirectToAction("Index");
            }

            var pokemonDetails = _pokeApiService.GetPokemonDetailsByName(pokemonName);
            var pokemonDetailsViewModel = (PokemonDetailsViewModel) pokemonDetails;
            pokemonDetailsViewModel =  _pokeApiService.AddClassesToPokemonDetailsViewModel(pokemonDetailsViewModel);

            return View(pokemonDetailsViewModel);
        }
    }
}
