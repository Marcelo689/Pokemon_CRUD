using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class PokemonTypesController : Controller
    {
        private readonly PokemonContext _context;

        public PokemonTypesController(PokemonContext context)
        {
            _context = context;
        }

        // GET: PokemonTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PokemonType.ToListAsync());
        }

        // GET: PokemonTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemonType == null)
            {
                return NotFound();
            }

            return View(pokemonType);
        }

        // GET: PokemonTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PokemonTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PokemonType pokemonType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemonType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokemonType);
        }

        // GET: PokemonTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonType.FindAsync(id);
            if (pokemonType == null)
            {
                return NotFound();
            }
            return View(pokemonType);
        }

        // POST: PokemonTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PokemonType pokemonType)
        {
            if (id != pokemonType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemonType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonTypeExists(pokemonType.Id))
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
            return View(pokemonType);
        }

        // GET: PokemonTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemonType == null)
            {
                return NotFound();
            }

            return View(pokemonType);
        }

        // POST: PokemonTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemonType = await _context.PokemonType.FindAsync(id);
            if (pokemonType != null)
            {
                _context.PokemonType.Remove(pokemonType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonTypeExists(int id)
        {
            return _context.PokemonType.Any(e => e.Id == id);
        }
    }
}
