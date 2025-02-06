using System.Text.Json.Serialization;

namespace WebMVC.ApiModels
{
    public class PokemonDetail
    {
        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }
    }

}
