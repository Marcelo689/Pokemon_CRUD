using ApiModelsResponse.ApiModels.ApiResponse;
using System.Text.Json.Serialization;

namespace ApiModelsResponse.ApiModels
{
    public class PokemonDetailSprites
    {
        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }
    }

}
