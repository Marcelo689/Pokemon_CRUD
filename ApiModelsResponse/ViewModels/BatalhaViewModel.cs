using ApiModelsResponse.ViewModels;

namespace ModelsResponse.ViewModels
{
    public class BatalhaViewModel
    {
        public List<TreinadorViewModel> TreinadoresViewModel = new();
        public List<PokemonCampoBatalhaViewModel> PokemonsTrainer1 = new();
        public List<PokemonCampoBatalhaViewModel> PokemonsTrainer2 = new();
        public PokemonCampoBatalhaViewModel PokemonCampoTrainer1 { get; set; }
        public PokemonCampoBatalhaViewModel PokemonCampoTrainer2 { get; set; }
    }
}
