namespace PokemonConsoleWorld
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class Pokemon
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("abilities")]
        public List<Ability> Abilities { get; set; }

        [JsonPropertyName("held_items")]
        public List<HeldItem> HeldItems { get; set; }

        [JsonPropertyName("moves")]
        public List<Move> Moves { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonConsoleWorld.Pokemon.PokemonType> Types { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        public class PokemonType
        {
            [JsonPropertyName("type")]
            public TypeInfo type;

            [JsonPropertyName("slot")]
            public int slot;
            public class TypeInfo
            {
                [JsonPropertyName("name")]
                public string name;

                [JsonPropertyName("url")]
                public string url;
            }
        }
        public class Ability
        {
            [JsonPropertyName("ability")]
            public AbilityInfo Abilities { get; set; }

            [JsonPropertyName("is_hidden")]
            public bool IsHidden { get; set; }

            [JsonPropertyName("slot")]
            public int Slot { get; set; }

            public class AbilityInfo
            {
                [JsonPropertyName("name")]
                public string Name { get; set; }
                [JsonPropertyName("url")]
                public string Url { get; set; }
            }
        }


        public class HeldItem
        {

            [JsonPropertyName("item")]
            public Item Items { get; set; }
            public List<VersionDetail> VersionDetails { get; set; }

            public class Item
            {
                [JsonPropertyName("name")]
                public string Name { get; set; }
                [JsonPropertyName("url")]
                public string Url { get; set; }
            }

            public class VersionDetail
            {
                [JsonPropertyName("rarity")]
                public int Rarity { get; set; }
            }
        }

        public class Move
        {
            [JsonPropertyName("move")]
            public MoveInfo Moves { get; set; }

            public class MoveInfo
            {
                [JsonPropertyName("name")]
                public string Name { get; set; }
                [JsonPropertyName("url")]
                public string Url { get; set; }
            }

        }

        public class Species
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public class Sprites
        {
            public string BackDefault { get; set; }
            public string BackFemale { get; set; }
            public string BackShiny { get; set; }
            public string BackShinyFemale { get; set; }
            public string FrontDefault { get; set; }
            public string FrontFemale { get; set; }
            public string FrontShiny { get; set; }
            public string FrontShinyFemale { get; set; }

        }
    }
}
