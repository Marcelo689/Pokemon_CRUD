using WebMVC.ViewModels;

namespace WebMVC.Models
{
    public class Pokemon
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public PokemonType? Type { get; set; }
        public PokemonType? SecondType { get; set; }
        public PokemonAbility? Ability { get; set; }

        public PokemonViewModel ToViewModel(Pokemon pokemon)
        {
            return new PokemonViewModel
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                PokemonType1 = pokemon.Type,
                PokemonType2 = pokemon.SecondType,
                PokemonAbility = pokemon.Ability,
            };
        }
    }

    public class PokemonType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
