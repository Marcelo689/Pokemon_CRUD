using WebMVC.ViewModels;

namespace WebMVC.Models
{
    public class Treinador
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Location { get; set; }
        public List<PokemonTreinadorRelacionado> Pokemons { get; set; }

        public static explicit operator Treinador(TreinadorViewModel v)
        {
            return new Treinador { Id = v.Id, Name = v.Name, ImagePath = v.ImagePath, Location = v.Location, };
        }
    }
}
