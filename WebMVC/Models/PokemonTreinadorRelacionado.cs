﻿namespace WebMVC.Models
{
    public class PokemonTreinadorRelacionado
    {
        public int Id { get; set; }
        public int TreinadorId { get; set; }
        public Treinador Treinador { get; set; }
        public Pokemon PokemonApiId { get; set; }
        public string Name { get; set; }
        public PokemonType? Type { get; set; }
        public int TypeId { get; set; }
        public int SecondTypeId { get; set; }
        public PokemonAbility? Ability { get; set; }
        public int AbilityId {  get; set; }    
        public int Level { get; set; }
        public int Move1Id { get; set; }
        public int Move2Id { get; set; }
        public int Move3Id { get; set; }
        public int Move4Id { get; set; }
    }

    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isPhysical { get; set; }
        public bool isSpecial { get; set; }
        public bool isStatus { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; } 
    }
}
