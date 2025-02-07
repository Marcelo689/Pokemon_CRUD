using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class TreinadorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Location { get; set; }
        public List<PokemonTreinadorViewModel> Pokemons { get; set; } = new List<PokemonTreinadorViewModel>();
    }
}
