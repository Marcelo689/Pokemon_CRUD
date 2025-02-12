using System.ComponentModel.DataAnnotations.Schema;

namespace ApiModelsResponse.ViewModels
{
    public class TreinadorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Location { get; set; }
        public List<PokemonDetailsViewModel> Pokemons { get; set; } = new();

        // Pokémon 1
        public string Pokemon1Name { get; set; }
        public string Pokemon1Ability { get; set; }
        public string Pokemon1PokemonTypeName1 { get; set; }
        public string Pokemon1PokemonTypeName2 { get; set; }
        public string Pokemon1Move1 { get; set; }
        public string Pokemon1Move2 { get; set; }
        public string Pokemon1Move3 { get; set; }
        public string Pokemon1Move4 { get; set; }
        public int Pokemon1Level { get; set; }

        // Pokémon 2
        public string Pokemon2Name { get; set; }
        public string Pokemon2Ability { get; set; }
        public string Pokemon2PokemonTypeName1 { get; set; }
        public string Pokemon2PokemonTypeName2 { get; set; }
        public string Pokemon2Move1 { get; set; }
        public string Pokemon2Move2 { get; set; }
        public string Pokemon2Move3 { get; set; }
        public string Pokemon2Move4 { get; set; }
        public int Pokemon2Level { get; set; }

        // Pokémon 3
        public string Pokemon3Name { get; set; }
        public string Pokemon3Ability { get; set; }
        public string Pokemon3PokemonTypeName1 { get; set; }
        public string Pokemon3PokemonTypeName2 { get; set; }
        public string Pokemon3Move1 { get; set; }
        public string Pokemon3Move2 { get; set; }
        public string Pokemon3Move3 { get; set; }
        public string Pokemon3Move4 { get; set; }
        public int Pokemon3Level { get; set; }

        // Pokémon 4
        public string Pokemon4Name { get; set; }
        public string Pokemon4Ability { get; set; }
        public string Pokemon4PokemonTypeName1 { get; set; }
        public string Pokemon4PokemonTypeName2 { get; set; }
        public string Pokemon4Move1 { get; set; }
        public string Pokemon4Move2 { get; set; }
        public string Pokemon4Move3 { get; set; }
        public string Pokemon4Move4 { get; set; }
        public int Pokemon4Level { get; set; }

        // Pokémon 5
        public string Pokemon5Name { get; set; }
        public string Pokemon5Ability { get; set; }
        public string Pokemon5PokemonTypeName1 { get; set; }
        public string Pokemon5PokemonTypeName2 { get; set; }
        public string Pokemon5Move1 { get; set; }
        public string Pokemon5Move2 { get; set; }
        public string Pokemon5Move3 { get; set; }
        public string Pokemon5Move4 { get; set; }
        public int Pokemon5Level { get; set; }

        // Pokémon 6
        public string Pokemon6Name { get; set; }
        public string Pokemon6Ability { get; set; }
        public string Pokemon6PokemonTypeName1 { get; set; }
        public string Pokemon6PokemonTypeName2 { get; set; }
        public string Pokemon6Move1 { get; set; }
        public string Pokemon6Move2 { get; set; }
        public string Pokemon6Move3 { get; set; }
        public string Pokemon6Move4 { get; set; }
        public int Pokemon6Level { get; set; }

        public bool IsValid()
        {
            if(Pokemon1Name is not null)
            {
                if(Pokemon1Ability is null)
                    return false;

                if (Pokemon1Level == 0)
                    return false;
            }
            if (Pokemon2Name is not null)
            {
                if (Pokemon2Ability is null)
                    return false;

                if (Pokemon2Level == 0)
                    return false;
            }
            if (Pokemon3Name is not null)
            {
                if (Pokemon3Ability is null)
                    return false;

                if (Pokemon3Level == 0)
                    return false;
            }
            if (Pokemon4Name is not null)
            {
                if (Pokemon4Ability is null)
                    return false;

                if (Pokemon4Level == 0)
                    return false;
            }
            if (Pokemon5Name is not null)
            {
                if (Pokemon5Ability is null)
                    return false;

                if (Pokemon5Level == 0)
                    return false;
            }
            if (Pokemon6Name is not null)
            {
                if (Pokemon6Ability is null)
                    return false;

                if (Pokemon6Level == 0)
                    return false;
            }

            return Name is not null || Name != "";
        }
    }
}
