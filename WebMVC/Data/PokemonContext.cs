using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.Data
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para PokemonTreinador e relação com Treinador
            modelBuilder.Entity<PokemonTreinadorRelacionado>()
                .HasOne(p => p.Treinador)
                .WithMany(t => t.Pokemons)
                .HasForeignKey(p => p.TreinadorId)
                .OnDelete(DeleteBehavior.Restrict); // Restringir deleção em cascata
        }

        public DbSet<PokemonAbility> PokemonAbility{ get; set; }
        public DbSet<PokemonType> PokemonType{ get; set; }
        public DbSet<Move> Move{ get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Treinador> Treinador{ get; set; }
        public DbSet<PokemonTreinadorRelacionado> PokemonTreinador{ get; set; }
        public DbSet<PokemonDetails> PokemonStatsDetails { get; set; }

    }
}
