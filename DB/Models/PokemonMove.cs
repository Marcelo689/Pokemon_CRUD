namespace DB.Models
{
    public class PokemonMove
    {
        public int Id { get; set; }
        public bool isPhysical;
        public bool isStatus;
        public string PokemonName { get; set; }
        public string MoveName { get; set; }
        public int MoveId { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
        public bool IsSpecial { get; set; }
        public string ShortDescription { get; set; }
    }
}
