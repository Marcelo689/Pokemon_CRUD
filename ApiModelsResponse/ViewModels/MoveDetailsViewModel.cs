namespace ModelsResponse.ViewModels
{
    public class MoveDetailsViewModel
    {
        public int? Power { get; set; }
        public string Name { get; set; }
        public int? Accuracy { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsStatus { get; set; }
        public string Description { get; set; }
        public string ShorDescription { get; set; }
        public int? PP { get; set; }
        public int Id { get; set; }
    }
}
