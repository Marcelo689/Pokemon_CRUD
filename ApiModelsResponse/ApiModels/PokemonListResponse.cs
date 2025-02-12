using System.Text.Json.Serialization;

namespace ApiModelsResponse.ApiModels
{
    public class PokemonListResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; } // Representa o total de Pokémon

        [JsonPropertyName("next")]
        public string Next { get; set; } // URL para a próxima página (se houver)

        [JsonPropertyName("previous")]
        public string Previous { get; set; } // URL para a página anterior (se houver)

        [JsonPropertyName("results")]
        public List<PokemonListItem> Results { get; set; } // Lista de Pokémon
    }
}
