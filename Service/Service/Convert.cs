using ApiModelsResponse.ViewModels;
using DB.Models;

namespace Service.Service
{
    public static class C
    {
        public static PokemonAbilityViewModel Convert(PokemonAbility pokemonAbility)
        {
            return new PokemonAbilityViewModel()
            {
                Id = pokemonAbility.Id,
                Name = pokemonAbility.Name,
            };
        }
        public static PokemonTypeViewModel Convert(PokemonType pokemonAbility)
        {
            return new PokemonTypeViewModel()
            {
                Id = pokemonAbility.Id,
                Name = pokemonAbility.Name,
            };
        }
        public static PokemonDetailsViewModel Convert(PokemonDetails v)
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
