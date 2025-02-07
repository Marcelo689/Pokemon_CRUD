using System.Text.Json.Serialization;
using WebMVC.ApiModels.ApiResponse;

namespace WebMVC.ApiModels
{
    public class PokemonDetailSprites
    {
        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }
    }

}
