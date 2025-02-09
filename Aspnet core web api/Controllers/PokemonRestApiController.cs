using DB.Data;
using Microsoft.AspNetCore.Mvc;
using ModelsResponse.ViewModels;
using WebMVC.Service;

namespace DB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonRestApiController : ControllerBase
    {
        private readonly PokeApiService _pokeApiService;
        private readonly PokemonContext _context;

        public PokemonRestApiController(PokeApiService pokeApiService, PokemonContext context)
        {
            _pokeApiService = pokeApiService;
            _context = context;
        }

        [HttpGet("movimentos/{pokemonId}")]
        public IActionResult GetMovimentos(int pokemonId)
        {
            var pokemon = _context.Pokemon.First(e => e.Id == pokemonId);
            string pokemonName = pokemon.Name;

            List<MoveViewModel> listaMoves = _pokeApiService.GetMovesFromPokemon(pokemonName);

            return Ok(listaMoves);
        }
    }
}
