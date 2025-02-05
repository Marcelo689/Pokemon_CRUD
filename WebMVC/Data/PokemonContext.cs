using Microsoft.EntityFrameworkCore;
using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC.Data
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options)
        {
            
        }
        public DbSet<Pokemon> Pokemon{ get; set; }
        public DbSet<PokemonAbility> PokemonAbility{ get; set; }
        public DbSet<PokemonType> PokemonType{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>().ToTable("Pokemon");
            modelBuilder.Entity<PokemonAbility>().ToTable("PokemonAbility");
            modelBuilder.Entity<PokemonType>().ToTable("PokemonType");
        }

        public PokemonViewModel GetTypeAndAbilities(PokemonContext context)
        {
            PokemonViewModel model = new PokemonViewModel();
            model.PokemonAbilities = context.PokemonAbility.ToList();
            model.PokemonTypes = context.PokemonType.ToList();
            return model;
        }
    }
}
