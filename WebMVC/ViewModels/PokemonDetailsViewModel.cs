using System.ComponentModel.DataAnnotations.Schema;
using WebMVC.Models;

namespace WebMVC.ViewModels
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
        public static explicit operator PokemonDetailsViewModel(PokemonDetails v)
        {
            return new PokemonDetailsViewModel
            {
                Id = v.Id,
                HP = v.HP,
                ATTACK = v.ATTACK,
                DEFENSE = v.DEFENSE,
                SP_ATTACK = v.SP_ATTACK,
                SP_DEFENSE = v.SP_DEFENSE,
                SPEED = v.SPEED,
                height = v.height,
                weight = v.weight,
                Ability1Id = v.Ability1Id,
                Ability2Id = v.Ability2Id,
                Ability3Id = v.Ability3Id,
                ImageUrl = v.ImageUrl,
                Name = v.Name,
                PokemonTypeId1 = v.PokemonTypeId1,
                PokemonTypeId2 = v.PokemonTypeId2
            };
        }
    }
}
