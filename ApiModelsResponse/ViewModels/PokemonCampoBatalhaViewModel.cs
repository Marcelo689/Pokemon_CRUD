using ApiModelsResponse.ViewModels;

namespace ModelsResponse.ViewModels
{
    public enum Status
    {
        Paralizado,
        Envenenado,
        Queimado,
        Dormindo
    }
    public class PokemonCampoBatalhaViewModel
    {
        public int Vida { get; set; }
        public Status Status { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public int AbilityId { get; set; }
        public PokemonAbilityViewModel Ability { get; set; }
        public int? PokemonTypeId1 { get; set; }
        public PokemonTypeViewModel PokemonType1 { get; set; }
        public int? PokemonTypeId2 { get; set; }
        public PokemonTypeViewModel? PokemonType2 { get; set; }
        public int HP { get; set; }
        public int ATTACK { get; set; }
        public int DEFENSE { get; set; }
        public int SP_ATTACK { get; set; }
        public int SP_DEFENSE { get; set; }
        public int SPEED { get; set; }
        public string ImageUrl { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        public int? Move1 { get; set; }
        public int? Move2 { get; set; }
        public int? Move3 { get; set; }
        public int? Move4 { get; set; }
    }
}
