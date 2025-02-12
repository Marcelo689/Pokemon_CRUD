namespace ModelsResponse.ViewModels
{
    public class MoveViewModel
    {
        public string ShortDescription { get; set; }
        public int? PP { get; set; }
        public bool IsStatus { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsSpecial { get; set; }
        public int? Accuracy { get; set; }
        public int? Power { get; set; }
        public string PokemonName { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
