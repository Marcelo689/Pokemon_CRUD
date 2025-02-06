using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Data;
using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly PokemonContext _context;

        public PokemonsController(PokemonContext context)
        {
            _context = context;
        }

        // GET: Pokemons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pokemon.ToListAsync());
        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // GET: Pokemons/Create
        public IActionResult Create()
        {
            PokemonViewModel model = _context.GetTypeAndAbilities(_context);
            return View(model);
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( PokemonViewModel pokemon)
        {
            if (pokemon.IsValid())
            {
                Pokemon pokemonDb = pokemon.ToModel(pokemon);
                SetAbilityAndTypes(pokemon, pokemonDb);
                _context.Add(pokemonDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Redirect("Create");
            }
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.FindAsync(id);

            PokemonViewModel pokemonViewModel = _context.GetTypeAndAbilities(_context);
            PokemonViewModel pokemonViewModelProp = pokemon.ToViewModel(pokemon);

            pokemonViewModelProp.PokemonAbilities = pokemonViewModel.PokemonAbilities;
            pokemonViewModelProp.PokemonTypes = pokemonViewModel.PokemonTypes;

            if (pokemonViewModelProp is null)
            {
                return NotFound();
            }
            return View(pokemonViewModelProp);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PokemonViewModel pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (pokemon.IsValid())
            {
                try
                {
                    Pokemon pokemonModel = pokemon.ToModel(pokemon);
                    SetAbilityAndTypes(pokemon, pokemonModel);

                    _context.Update(pokemonModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            GetPokemonAbilityAndTypes(pokemon);
            return View(pokemon);
        }

        private void GetPokemonAbilityAndTypes(PokemonViewModel pokemon)
        {
            PokemonViewModel pokemonAbilityType = _context.GetTypeAndAbilities(_context);
            pokemon.PokemonAbilities = pokemonAbilityType.PokemonAbilities;
            pokemon.PokemonTypes = pokemonAbilityType.PokemonTypes;
        }

        private void SetAbilityAndTypes(PokemonViewModel pokemon, Pokemon pokemonModel)
        {
            pokemonModel.Ability = _context.PokemonAbility.First(e => e.Id == pokemon.PokemonIntAbility);
            pokemonModel.Type = _context.PokemonType.First(e => e.Id == pokemon.PokemonIntType1);
            if(pokemon.PokemonIntType2 != 0)
                pokemonModel.SecondType = _context.PokemonType.First(e => e.Id == pokemon.PokemonIntType2);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemon.Remove(pokemon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(e => e.Id == id);
        }
    }
}
