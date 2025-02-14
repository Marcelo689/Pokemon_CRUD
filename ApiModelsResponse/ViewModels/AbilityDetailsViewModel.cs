
namespace ApiModelsResponse.ViewModels
{
    public class AbilityDetailsViewModel
    {
        public string Name { get; set; }

        public string EffectEntries { get; set; }

        public List<PokemonAbilityViewModel> Pokemon { get; set; }

        public string Description { get; set; }  // Description of the ability
    }
}
