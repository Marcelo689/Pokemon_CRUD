using System.Text.Json.Serialization;

namespace ApiModelsResponse.ApiModels
{
    public class PokemonListItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
