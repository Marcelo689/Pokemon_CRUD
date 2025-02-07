namespace WebMVC.Models
{
    public class Pokemon
    {
        public int Id { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Url { get; set; } // URL to fetc
        public List<PokemonTreinadorRelacionado> Pokemons { get; set; }
    }
    public class PokemonStatsDetails
    {
        public int Id { get; set; } // Primary Key
        public Pokemon Pokemon { get; set; }
        public int PokemonId { get; set; } // Foreign Key to Pokemon
        public string Type { get; set; }
        public string Abilities { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public PokemonApiStat Stats { get; set; }
    }
    public class PokemonApiStat
    {
        public int Id { get; set; }
        public int Stat_HP{ get; set; }
        public int Stat_ATTACK { get; set; }
        public int Stat_SP_ATTACK { get; set; }
        public int Stat_DEFENSE { get; set; }
        public int Stat__SP_DEFENSE { get; set; }
        public int Stat_SPEED { get; set; }
    }

}
