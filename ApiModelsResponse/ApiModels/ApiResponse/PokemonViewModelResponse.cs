using System.Text.Json.Serialization;

namespace ApiModelsResponse.ApiModels.ApiResponse
{
    public class PokemonViewModelResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class MoveViewModelResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

}
