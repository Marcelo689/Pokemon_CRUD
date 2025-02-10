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

        [HttpGet("moves/{pokemonName}")]
        public IActionResult GetMovimentos(string pokemonName)
        {
            try
            {
                var listaMoves = _pokeApiService.GetMovesFromPokemon(pokemonName)?.ToList();
                if (listaMoves == null || !listaMoves.Any())
                {
                    return Ok(new List<MoveViewModel>()); // Retorna uma lista vazia como JSON
                }

                return Ok(listaMoves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message }); // Retorna erro em formato JSON
            }
        }

    }
}
