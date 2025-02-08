using System.Text.Json.Serialization;

namespace ApiModelsResponse.ApiModels.ApiResponse
{
    public class TypeListResponse
    {
        [JsonPropertyName("results")]
        public List<TypeResponse> TypeListResponseProp { get; set; }
    }

    public class TypeResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
