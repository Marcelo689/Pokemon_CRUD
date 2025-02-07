using System.Text.Json.Serialization;

namespace WebMVC.ApiModels.ApiResponse
{

    public class PokemonDetailsResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("abilities")]
        public List<PokemonAbility> Abilities { get; set; }  // Modificado para refletir o JSON correto

        [JsonPropertyName("types")]
        public List<PokemonType> Types { get; set; }  // Modificado para refletir a estrutura correta

        [JsonPropertyName("stats")]
        public List<PokemonStat> Stats { get; set; }  // Corrigido para lista de objetos

        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }  // Corrigido para capturar imagens corretamente

        public string ImageUrl => Sprites?.Other?.OfficialArtwork?.FrontDefault;
        public int weight { get; set; }
        public int height { get; set; }
    }

    public class PokemonAbility
    {
        [JsonPropertyName("ability")]
        public AbilityInfo Ability { get; set; }
    }

    public class AbilityInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class PokemonType
    {
        [JsonPropertyName("type")]
        public TypeInfo Type { get; set; }
    }

    public class TypeInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class PokemonStat
    {
        [JsonPropertyName("stat")]
        public StatInfo Stat { get; set; }

        [JsonPropertyName("base_stat")]
        public int BaseStat { get; set; }
        [JsonPropertyName("effort")]
        public int effort { get; set; }
    }


    public class StatInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Sprites
    {
        [JsonPropertyName("other")]
        public OtherSprites Other { get; set; }
    }

    public class OtherSprites
    {
        [JsonPropertyName("official-artwork")]
        public OfficialArtwork OfficialArtwork { get; set; }
    }

    public class OfficialArtwork
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }
    }
}



