﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models
{
    public class PokemonDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ability1Id { get; set; }
        [NotMapped]
        public PokemonAbility Ability1 { get; set; }
        public int Ability2Id { get; set; }
        [NotMapped]
        public PokemonAbility? Ability2 { get; set; }
        public int Ability3Id { get; set; }
        [NotMapped]
        public PokemonAbility? Ability3 { get; set; }
        public int? PokemonTypeId1 { get; set; }
        [NotMapped]
        public PokemonType PokemonType1 { get; set; }
        public int? PokemonTypeId2 { get; set; }

        [NotMapped]
        public PokemonType? PokemonType2 { get; set; }
        public int HP { get; set; }
        public int ATTACK { get; set; }
        public int DEFENSE { get; set; }
        public int SP_ATTACK { get; set; }
        public int SP_DEFENSE { get; set; }
        public int SPEED { get; set; }
        public string ImageUrl { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        public int PokemonId { get; set; }
    }
}
