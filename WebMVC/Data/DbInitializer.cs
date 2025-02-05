using WebMVC.Models;

namespace WebMVC.Data
{
    public static class DbInitializer
    {
        public static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<PokemonContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void Initialize(PokemonContext context)
        {
            context.Database.EnsureCreated();


            if (context.Pokemon.Any())
            {
                return;
            }
                var abilities = new PokemonAbility
            {
                Name = "Blaze",
                Description = "Blaze increases the power of Fire-type moves by 50% (1.5×) when the ability-bearer's HP falls below a third of its maximum (known in-game as in a pinch)."
            };

            var pokemonTypes = new PokemonType
            {
                Name = "Fire"
            };

            var pokemons = new Pokemon
            {
                Name = "Charmeleon",
                Ability = abilities,
                Type = pokemonTypes
            };


            if (!context.Pokemon.Any())
            {
                context.Pokemon.Add(pokemons);
                context.SaveChanges();
            }

        }
    }
}
