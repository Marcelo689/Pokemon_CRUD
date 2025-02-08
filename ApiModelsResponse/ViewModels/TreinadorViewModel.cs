namespace ApiModelsResponse.ViewModels
{
    public class TreinadorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Location { get; set; }
        public List<PokemonDetailsViewModel> Pokemons { get; set; } = new();

        public bool IsValid()
        {
            return Name is not null || Name != "";
        }
    }
}
