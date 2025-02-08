using ApiModelsResponse.ViewModels;
using DB.Data;
using DB.Models;
using Service.Service;

namespace WebMVC.Service
{
    public class TreinadorService
    {
        private readonly PokemonContext _context;
        public TreinadorService(PokemonContext context)
        {
            _context = context;
        }

        public void AddTreinador(Treinador treinadorModel)
        {
            _context.Add(treinadorModel);
            _context.SaveChanges();
        }
        public PokemonDetailsViewModel AddClassesToPokemonDetailsViewModel(PokemonDetailsViewModel pokemonDetailsViewModel)
        {
            if(pokemonDetailsViewModel.Ability1Id != 0)
            pokemonDetailsViewModel.Ability1 =  C.Convert(_context.PokemonAbility.First(e => e.Id == pokemonDetailsViewModel.Ability1Id));
            if (pokemonDetailsViewModel.Ability2Id != 0)
                pokemonDetailsViewModel.Ability2 = C.Convert(_context.PokemonAbility.First(e => e.Id == pokemonDetailsViewModel.Ability2Id));
            if (pokemonDetailsViewModel.Ability3Id != 0)
                pokemonDetailsViewModel.Ability3 = C.Convert(_context.PokemonAbility.First(e => e.Id == pokemonDetailsViewModel.Ability3Id));

            pokemonDetailsViewModel.PokemonType1 = C.Convert(_context.PokemonType.First(e => e.Id == pokemonDetailsViewModel.PokemonTypeId1));

            if (pokemonDetailsViewModel.PokemonTypeId2 is not null)
                pokemonDetailsViewModel.PokemonType2 = C.Convert(_context.PokemonType.First(e => e.Id == pokemonDetailsViewModel.PokemonTypeId2));

            return pokemonDetailsViewModel;
        }
        
        public void FillTreinadorViewModel(TreinadorViewModel treinadorViewModel)
        {
            treinadorViewModel.Pokemons = _context.PokemonStatsDetails.Select( e => C.Convert(e)).ToList();
            foreach (var pokemon in treinadorViewModel.Pokemons)
            {
                AddClassesToPokemonDetailsViewModel(pokemon);
            }
        }
    }
}
