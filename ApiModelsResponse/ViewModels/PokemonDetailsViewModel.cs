using System.ComponentModel.DataAnnotations.Schema;

namespace ApiModelsResponse.ViewModels
{
    public class PokemonDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ability1Id { get; set; }
        [NotMapped]
        public PokemonAbilityViewModel Ability1 { get; set; }
        public int Ability2Id { get; set; }
        [NotMapped]
        public PokemonAbilityViewModel? Ability2 { get; set; }
        public int Ability3Id { get; set; }
        [NotMapped]
        public PokemonAbilityViewModel? Ability3 { get; set; }
        public int? PokemonTypeId1 { get; set; }
        [NotMapped]
        public PokemonTypeViewModel PokemonType1 { get; set; }
        public int? PokemonTypeId2 { get; set; }

        [NotMapped]
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
    }
}
