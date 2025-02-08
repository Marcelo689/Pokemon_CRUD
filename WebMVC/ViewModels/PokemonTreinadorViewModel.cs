using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class PokemonTreinadorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PokemonTypeViewModel? Type { get; set; }
        public PokemonTypeViewModel? SecondType { get; set; }
        public PokemonAbilityViewModel? Ability { get; set; }
        public int Level { get; set; }
        public int Move1Id { get; set; }
        public string Move1 { get; set; }
        public int Move2Id { get; set; }
        public string Move2 { get; set; }
        public int Move3Id { get; set; }
        public string Move3 { get; set; }
        public int Move4Id { get; set; }
        public string Move4 { get; set; }
    }

    public class PokemonTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator PokemonTypeViewModel(PokemonType v)
        {
            return new PokemonTypeViewModel { Id = v.Id, Name = v.Name };
        }
    }

    public class PokemonAbilityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator PokemonAbilityViewModel(PokemonAbility v)
        {
            return new PokemonAbilityViewModel { Id = v.Id, Name = v.Name };
        }
    }
}
