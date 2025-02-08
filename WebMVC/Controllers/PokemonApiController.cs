using ApiModelsResponse.ViewModels;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
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

        public static PokemonDetailsViewModel Convert(PokemonDetails v)
        {
            return new PokemonDetailsViewModel
            {
                Id = v.Id,
                HP = v.HP,
                ATTACK = v.ATTACK,
                DEFENSE = v.DEFENSE,
                SP_ATTACK = v.SP_ATTACK,
                SP_DEFENSE = v.SP_DEFENSE,
                SPEED = v.SPEED,
                height = v.height,
                weight = v.weight,
                Ability1Id = v.Ability1Id,
                Ability2Id = v.Ability2Id,
                Ability3Id = v.Ability3Id,
                ImageUrl = v.ImageUrl,
                Name = v.Name,
                PokemonTypeId1 = v.PokemonTypeId1,
                PokemonTypeId2 = v.PokemonTypeId2
            };
        }
        public async Task<ActionResult> Details(string pokemonName)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                return RedirectToAction("Index");
            }

            var pokemonDetails = _pokeApiService.GetPokemonDetailsByName(pokemonName);
            var pokemonDetailsViewModel = Convert(pokemonDetails);
            pokemonDetailsViewModel =  _pokeApiService.AddClassesToPokemonDetailsViewModel(pokemonDetailsViewModel);

            return View(pokemonDetailsViewModel);
        }
    }
}
