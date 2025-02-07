using System.Text.Json.Serialization;

namespace WebMVC.ApiModels.ApiResponse
{
    public class AbilityListResponse
    {

        [JsonPropertyName("results")]
        public List<AbilityResponse> Results { get; set; }
    }

    public class AbilityResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } 
    }
    
}
