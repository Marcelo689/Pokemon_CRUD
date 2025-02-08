using DB.Data;
using Microsoft.AspNetCore.Mvc;

namespace DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonRestApiController : ControllerBase
    {
        private readonly PokemonContext _context;

        public PokemonRestApiController(PokemonContext context)
        {
            _context = context;
        }

        //[HttpGet("movimentos/{pokemonId}")]
        //public IActionResult GetMovimentos(int pokemonId)
        //{
        //    var pokemon = _context.Pokemon.First( e => e.Id == pokemonId);


        //    var pokemon = _context.PokemonStatsDetails
        //        .Include(p => p.Movimentos)  // Supondo que você tenha uma relação com a tabela de movimentos
        //        .FirstOrDefault(p => p.Id == pokemonId);

        //    if (pokemon == null)
        //        return NotFound();

        //    return Ok(pokemon.Movimentos);
        //}
    }
}
