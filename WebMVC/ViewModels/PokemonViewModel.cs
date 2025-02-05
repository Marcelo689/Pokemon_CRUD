using Microsoft.IdentityModel.Tokens;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class PokemonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PokemonType? PokemonType1 { get; set; }
        public int PokemonIntType1 { get; set; }
        public PokemonType? PokemonType2 { get; set; }
        public int PokemonIntType2 { get; set; }
        public PokemonAbility? PokemonAbility { get; set; }
        public int PokemonIntAbility { get; set; }
        public List<PokemonAbility> PokemonAbilities { get; set; } = new List<PokemonAbility>();
        public List<PokemonType> PokemonTypes { get; set; } = new List<PokemonType>();

        public Pokemon ToModel(PokemonViewModel pokemon)
        {
            return new Pokemon
            {
                Id = pokemon.Id,
                Ability = pokemon.PokemonAbility,
                Name = pokemon.Name,
                Type = pokemon.PokemonType1,
                SecondType = pokemon.PokemonType2,

            };
        }

        public bool IsValid()
        {
            bool nomePreenchido = !this.Name.IsNullOrEmpty();
            bool possuiTipo = this.PokemonIntType1 is not 0;
            bool possuiHabilidade = this.PokemonIntAbility is not 0;
            return nomePreenchido && possuiTipo && possuiHabilidade;
        }
    }
}
