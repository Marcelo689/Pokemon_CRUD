using ApiModelsResponse.ViewModels;
using DB.Data;
using DB.Models;
using Microsoft.EntityFrameworkCore;
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
        
        public TreinadorViewModel FillTreinadorViewModel(TreinadorViewModel treinadorViewModel)
        {
            treinadorViewModel.Pokemons = _context.PokemonStatsDetails.Select( e => C.Convert(e)).ToList();
            foreach (var pokemon in treinadorViewModel.Pokemons)
            {
                AddClassesToPokemonDetailsViewModel(pokemon);
            }

            return treinadorViewModel;
        }

        public Move? GetMoveByName(string pokemon1Move1)
        {
            if (pokemon1Move1 is null)
                return null;

            return _context.Move.First(e => e.Name == pokemon1Move1);
        }

        public PokemonAbility GetAbilityFromName(string pokemon1Ability)
        {
            return _context.PokemonAbility.First(e => e.Name == pokemon1Ability);
        }

        public Pokemon GetPokemonFromName(string name)
        {
            return _context.Pokemon.First(e => e.Name == name);
        }

        public PokemonType GetPokemonTypeFromName(string pokemon1PokemonTypeName)
        {
            if (pokemon1PokemonTypeName is null)
                return null;
            return _context.PokemonType.First(e => e.Name == pokemon1PokemonTypeName);
        }

        public void AddTreinadorAndPokemon(Treinador treinadorModel, PokemonTreinadorRelacionado pokemon1, bool isEdit)
        {
            if(isEdit)
            {
                var hasPokemon = _context.PokemonTreinador.Any(e => e.TreinadorId == treinadorModel.Id && pokemon1.PokemonApiId == e.PokemonApiId
                    && e.AbilityId == pokemon1.AbilityId);
                _context.Update(pokemon1);
            }
            else
            {
                _context.Treinador.Add(treinadorModel);
            }
            _context.PokemonTreinador.Add(pokemon1);
            _context.SaveChanges();
        }

        public List<TreinadorViewModel> GetAllTrainers()
        {
            return _context.Treinador.Select(e => new TreinadorViewModel
            {
                Id = e.Id,
                Name = e.Name,
                ImagePath = e.ImagePath,
                Location = e.Location
            }).ToList();
        }

        public TreinadorViewModel GetTreinadorPokemonsData(TreinadorViewModel treinadorViewModel)
        {
            var listaPokemonDoTreinador = _context.PokemonTreinador.Where(e => e.TreinadorId == treinadorViewModel.Id && e.Treinador.Name == treinadorViewModel.Name).Include(e => e.Treinador).Include(e => e.PokemonApiId).Include(e => e.Type).Include(e => e.Ability);
            var listaTipos = _context.PokemonType.ToList();
            var listaMovimentos = _context.Move.ToList();
            int contador = 1;
            foreach (PokemonTreinadorRelacionado pokemon in listaPokemonDoTreinador)
            {
                if(contador == 1)
                {
                    treinadorViewModel.Pokemon1Name = pokemon.PokemonApiId.Name;
                    treinadorViewModel.Pokemon1Ability = pokemon.Ability.Name;
                    treinadorViewModel.Pokemon1PokemonTypeName1 = pokemon.Type.Name;
                    treinadorViewModel.Pokemon1PokemonTypeName2 = listaTipos.First( e=> e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    treinadorViewModel.Pokemon1Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    treinadorViewModel.Pokemon1Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    treinadorViewModel.Pokemon1Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    treinadorViewModel.Pokemon1Move4 = listaMovimentos.First(e => e.Id == pokemon.Move4Id)?.Name ?? "";
                    treinadorViewModel.Pokemon1Level = pokemon.Level;
                    contador++;
                    continue;
                }

                if (contador == 2)
                {
                    treinadorViewModel.Pokemon2Name = pokemon.PokemonApiId.Name;
                    treinadorViewModel.Pokemon2Ability = pokemon.Ability.Name;
                    treinadorViewModel.Pokemon2PokemonTypeName1 = pokemon.Type.Name;
                    treinadorViewModel.Pokemon2PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    treinadorViewModel.Pokemon2Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    treinadorViewModel.Pokemon2Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    treinadorViewModel.Pokemon2Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    treinadorViewModel.Pokemon2Move4 = listaMovimentos.First(e => e.Id == pokemon.Move4Id)?.Name ?? "";
                    treinadorViewModel.Pokemon2Level = pokemon.Level;
                    contador++;
                    
                    continue;
                }

                if (contador == 3)
                {
                    treinadorViewModel.Pokemon3Name = pokemon.PokemonApiId.Name;
                    treinadorViewModel.Pokemon3Ability = pokemon.Ability.Name;
                    treinadorViewModel.Pokemon3PokemonTypeName1 = pokemon.Type.Name;
                    treinadorViewModel.Pokemon3PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    treinadorViewModel.Pokemon3Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    treinadorViewModel.Pokemon3Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    treinadorViewModel.Pokemon3Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    treinadorViewModel.Pokemon3Move4 = listaMovimentos.First(e => e.Id == pokemon.Move4Id)?.Name ?? "";
                    treinadorViewModel.Pokemon3Level = pokemon.Level;
                    contador++;
                    
                    continue;
                }

                if (contador == 4)
                {
                    treinadorViewModel.Pokemon4Name = pokemon.PokemonApiId.Name;
                    treinadorViewModel.Pokemon4Ability = pokemon.Ability.Name;
                    treinadorViewModel.Pokemon4PokemonTypeName1 = pokemon.Type.Name;
                    treinadorViewModel.Pokemon4PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    treinadorViewModel.Pokemon4Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    treinadorViewModel.Pokemon4Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    treinadorViewModel.Pokemon4Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    treinadorViewModel.Pokemon4Move4 = listaMovimentos.First(e => e.Id == pokemon.Move4Id)?.Name ?? "";
                    treinadorViewModel.Pokemon4Level = pokemon.Level;
                    contador++;
                    
                    continue;
                }

                if (contador == 5)
                {
                    treinadorViewModel.Pokemon5Name = pokemon.PokemonApiId.Name;
                    treinadorViewModel.Pokemon5Ability = pokemon.Ability.Name;
                    treinadorViewModel.Pokemon5PokemonTypeName1 = pokemon.Type.Name;
                    treinadorViewModel.Pokemon5PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    treinadorViewModel.Pokemon5Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    treinadorViewModel.Pokemon5Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    treinadorViewModel.Pokemon5Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    treinadorViewModel.Pokemon5Move4 = listaMovimentos.First(e => e.Id == pokemon.Move4Id)?.Name ?? "";
                    treinadorViewModel.Pokemon5Level = pokemon.Level;
                    contador++;
                    
                    continue;
                }

                if (contador == 6)
                {
                    treinadorViewModel.Pokemon6Name = pokemon.PokemonApiId.Name;
                    treinadorViewModel.Pokemon6Ability = pokemon.Ability.Name;
                    treinadorViewModel.Pokemon6PokemonTypeName1 = pokemon.Type.Name;
                    treinadorViewModel.Pokemon6PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    treinadorViewModel.Pokemon6Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    treinadorViewModel.Pokemon6Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    treinadorViewModel.Pokemon6Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    treinadorViewModel.Pokemon6Move4 = listaMovimentos.First(e => e.Id == pokemon.Move4Id)?.Name ?? "";
                    treinadorViewModel.Pokemon6Level = pokemon.Level;
                    contador++;
                    
                    continue;
                }
            }
            return treinadorViewModel;
        }

        public Treinador? GetTreinadorById(int id)
        {
            return _context.Treinador.FirstOrDefault(e => e.Id == id);
        }

        public PokemonTreinadorRelacionado GeTreinadorPokemonFromName(Treinador? treinadorModel, string? pokemonName, int pokemonIndex)
        {
            return _context.PokemonTreinador.Include(e => e.Treinador).ToList().FirstOrDefault(e => e.Treinador.Name == treinadorModel.Name &&  e.PokemonIndex == pokemonIndex);
        }

        public void UpdatePokemon(PokemonTreinadorRelacionado pokemonTreinador, TreinadorViewModel treinadorViewModel)
        {
            pokemonTreinador.Name = treinadorViewModel.Name;

            switch (pokemonTreinador.PokemonIndex)
            {
                case 1:
                    pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon1Ability);
                    pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon1PokemonTypeName1);
                    pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon1PokemonTypeName2).Id;
                    pokemonTreinador.Level = treinadorViewModel.Pokemon1Level;
                    pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon1Move1).Id;
                    pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon1Move2).Id;
                    pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon1Move3).Id;
                    pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon1Move4).Id;
                    break;
                case 2:
                    pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon2Ability);
                    pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon2PokemonTypeName1);
                    pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon2PokemonTypeName2).Id;
                    pokemonTreinador.Level = treinadorViewModel.Pokemon2Level;
                    pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon2Move1).Id;
                    pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon2Move2).Id;
                    pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon2Move3).Id;
                    pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon2Move4).Id;
                    break;
                case 3:
                    pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon3Ability);
                    pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon3PokemonTypeName1);
                    pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon3PokemonTypeName2).Id;
                    pokemonTreinador.Level = treinadorViewModel.Pokemon3Level;
                    pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon3Move1).Id;
                    pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon3Move2).Id;
                    pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon3Move3).Id;
                    pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon3Move4).Id;
                    break;
                case 4:
                    pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon4Ability);
                    pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon4PokemonTypeName1);
                    pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon4PokemonTypeName2).Id;
                    pokemonTreinador.Level = treinadorViewModel.Pokemon4Level;
                    pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon4Move1).Id;
                    pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon4Move2).Id;
                    pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon4Move3).Id;
                    pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon4Move4).Id;
                    break;
                case 5:
                    pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon5Ability);
                    pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon5PokemonTypeName1);
                    pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon5PokemonTypeName2).Id;
                    pokemonTreinador.Level = treinadorViewModel.Pokemon5Level;
                    pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon5Move1).Id;
                    pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon5Move2).Id;
                    pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon5Move3).Id;
                    pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon5Move4).Id;
                    break;
                case 6:
                    pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon6Ability);
                    pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon6PokemonTypeName1);
                    pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon6PokemonTypeName2).Id;
                    pokemonTreinador.Level = treinadorViewModel.Pokemon6Level;
                    pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon6Move1).Id;
                    pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon6Move2).Id;
                    pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon6Move3).Id;
                    pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon6Move4).Id;
                    break;
                default:
                    break;
            }

            _context.Update(pokemonTreinador);
            _context.SaveChanges();

        }

    }
}
