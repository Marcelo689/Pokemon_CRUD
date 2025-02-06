using System.Text.Json.Serialization;

namespace WebMVC.ApiModels
{
    public class PokemonViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string ImageUrl { get; set; }
    }

}
