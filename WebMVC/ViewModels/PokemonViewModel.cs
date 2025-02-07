using Microsoft.IdentityModel.Tokens;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class PokemonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public PokemonTypeViewModel? PokemonType1 { get; set; }
        public int PokemonIntType1 { get; set; }
        public PokemonTypeViewModel? PokemonType2 { get; set; }
        public int PokemonIntType2 { get; set; }
        public PokemonAbilityViewModel? PokemonAbility { get; set; }
        public int PokemonIntAbility { get; set; }
        public List<PokemonAbilityViewModel> PokemonAbilities { get; set; } = new List<PokemonAbilityViewModel>();
        public List<PokemonType> PokemonTypes { get; set; } = new List<PokemonType>();

        public PokemonTreinadorRelacionado ToModel(PokemonViewModel pokemon)
        {
            return new PokemonTreinadorRelacionado
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                AbilityId = pokemon.PokemonAbility.Id ,
                TypeId = pokemon.PokemonType1.Id,
                SecondTypeId = pokemon.PokemonType2.Id,
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
