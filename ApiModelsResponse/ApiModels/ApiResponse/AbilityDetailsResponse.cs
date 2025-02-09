using System.Text.Json.Serialization;

namespace ApiModelsResponse.ApiModels.ApiResponse
{
    public class AbilityDetailsResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("effect_entries")]
        public List<EffectEntries> EffectEntries { get; set; }

        [JsonPropertyName("pokemon")]
        public List<PokemonWhoHas> Pokemon { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }  // Description of the ability

        [JsonPropertyName("name")]
        public int Id { get; set; }
    }

    public class EffectEntries
    {

        [JsonPropertyName("effect")]
        public string Effect { get; set; }
    }

    public class PokemonWhoHas
    {
        [JsonPropertyName("pokemon")]
        public PokemonName Pokemon { get; set; }

    }

    public class PokemonName
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
