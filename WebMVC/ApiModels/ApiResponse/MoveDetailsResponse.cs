using System.Text.Json.Serialization;

namespace WebMVC.ApiModels.ApiResponse
{
    public class MoveDetailsResponse
    {
        [JsonPropertyName("accuracy")]
        public int? Accuracy { get; set; }

        [JsonPropertyName("type")]
        public MoveTypeResponse Type { get; set; }

        [JsonPropertyName("power")]
        public int? Power { get; set; }

        [JsonPropertyName("pp")]
        public int? PP { get; set; }

        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        [JsonPropertyName("damage_class")]
        public DamageClassResponse DamageClass { get; set; }

        [JsonPropertyName("learned_by_pokemon")]
        public List<PokemonViewModelResponse> LearnedByPokemon { get; set; } = new ();

        [JsonPropertyName("effect_entries")]
        public List<EffectEntriesResponse> EffectEntry { get; set; } = new();
    }
    public class DamageClassResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
    public class EffectEntriesResponse
    {
        [JsonPropertyName("effect")]
        public string Effect { get; set; }

        [JsonPropertyName("short_effect")]
        public string ShortEffect {  get; set; }
    }

    public class MoveListReponse
    {
        [JsonPropertyName("results")]
        public List<MoveViewModelResponse> Moves { get; set; }
    }

    public class MoveTypeResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
