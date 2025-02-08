namespace ApiModelsResponse.ViewModels
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
        public List<PokemonTypeViewModel> PokemonTypes { get; set; } = new List<PokemonTypeViewModel>();

        public bool IsValid()
        {
            bool nomePreenchido = this.Name is not null || this.Name != "";
            bool possuiTipo = this.PokemonIntType1 is not 0;
            bool possuiHabilidade = this.PokemonIntAbility is not 0;
            return nomePreenchido && possuiTipo && possuiHabilidade;
        }
    }
}
