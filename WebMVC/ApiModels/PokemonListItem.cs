using System.Text.Json.Serialization;

namespace WebMVC.ApiModels
{
    public class PokemonListItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
