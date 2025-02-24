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

        public PokemonType? GetPokemonTypeFromName(string pokemon1PokemonTypeName)
        {
            if (pokemon1PokemonTypeName is null)
                return null;
            return _context.PokemonType.First(e => e.Name == pokemon1PokemonTypeName);
        }

        public void AddTreinadorAndPokemon(Treinador treinadorModel, PokemonTreinadorRelacionado pokemon1, bool isEdit, int i)
        {
            if(isEdit)
            {
                var hasPokemon = _context.PokemonTreinador.Any(e => e.TreinadorId == treinadorModel.Id && e.PokemonIndex == i);
                if (hasPokemon)
                    _context.Update(pokemon1);
                else
                    _context.Add(pokemon1);
            }
            else
            {
                if(!_context.Treinador.Any( e => e.Id == treinadorModel.Id))
                    _context.Treinador.Add(treinadorModel);
                _context.PokemonTreinador.Add(pokemon1);
            }
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
                if (string.IsNullOrEmpty(pokemon.PokemonName))
                    continue;
                if(contador == 1)
                {
                    treinadorViewModel.Pokemon1Name = pokemon.PokemonApiId.Name;
                    treinadorViewModel.Pokemon1Ability = pokemon.Ability.Name;
                    treinadorViewModel.Pokemon1PokemonTypeName1 = pokemon.Type.Name;
                    if (pokemon.SecondTypeId != 0)
                        treinadorViewModel.Pokemon1PokemonTypeName2 = listaTipos.First( e=> e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    if (pokemon.Move1Id != 0)
                        treinadorViewModel.Pokemon1Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    if(pokemon.Move2Id != 0)
                        treinadorViewModel.Pokemon1Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    if (pokemon.Move3Id != 0)
                        treinadorViewModel.Pokemon1Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    if (pokemon.Move4Id != 0)
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
                    if (pokemon.SecondTypeId != 0)
                        treinadorViewModel.Pokemon2PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    if (pokemon.Move1Id != 0)
                        treinadorViewModel.Pokemon2Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    if (pokemon.Move2Id != 0)
                        treinadorViewModel.Pokemon2Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    if (pokemon.Move3Id != 0)
                        treinadorViewModel.Pokemon2Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    if (pokemon.Move4Id != 0)
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
                    if (pokemon.SecondTypeId != 0)
                        treinadorViewModel.Pokemon3PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    if (pokemon.Move1Id != 0)
                        treinadorViewModel.Pokemon3Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    if (pokemon.Move2Id != 0)
                        treinadorViewModel.Pokemon3Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    if (pokemon.Move3Id != 0)
                        treinadorViewModel.Pokemon3Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    if (pokemon.Move4Id != 0)
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
                    if(pokemon.SecondTypeId != 0)
                        treinadorViewModel.Pokemon4PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    if (pokemon.Move1Id != 0)
                        treinadorViewModel.Pokemon4Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    if (pokemon.Move2Id != 0)
                        treinadorViewModel.Pokemon4Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    if (pokemon.Move3Id != 0)
                        treinadorViewModel.Pokemon4Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    if (pokemon.Move4Id != 0)
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
                    if (pokemon.SecondTypeId != 0)
                        treinadorViewModel.Pokemon5PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    if (pokemon.Move1Id != 0)
                        treinadorViewModel.Pokemon5Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    if (pokemon.Move2Id != 0)
                        treinadorViewModel.Pokemon5Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    if (pokemon.Move3Id != 0)
                        treinadorViewModel.Pokemon5Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    if (pokemon.Move4Id != 0)
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
                    if (pokemon.SecondTypeId != 0)
                        treinadorViewModel.Pokemon6PokemonTypeName2 = listaTipos.First(e => e.Id == pokemon.SecondTypeId)?.Name ?? "";
                    if (pokemon.Move1Id != 0)
                        treinadorViewModel.Pokemon6Move1 = listaMovimentos.First(e => e.Id == pokemon.Move1Id)?.Name ?? "";
                    if (pokemon.Move2Id != 0)
                        treinadorViewModel.Pokemon6Move2 = listaMovimentos.First(e => e.Id == pokemon.Move2Id)?.Name ?? "";
                    if (pokemon.Move3Id != 0)
                        treinadorViewModel.Pokemon6Move3 = listaMovimentos.First(e => e.Id == pokemon.Move3Id)?.Name ?? "";
                    if (pokemon.Move4Id != 0)
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

        public PokemonTreinadorRelacionado GeTreinadorPokemonFromName(Treinador? treinadorModel, int pokemonIndex)
        {
            return _context.PokemonTreinador.Include(e => e.Treinador).ToList().FirstOrDefault(e => e.Treinador.Id == treinadorModel.Id &&  e.PokemonIndex == pokemonIndex);
        }

        public void UpdatePokemon(PokemonTreinadorRelacionado pokemonTreinador, string? pokemonName, TreinadorViewModel treinadorViewModel)
        {
            pokemonTreinador.TreinadorName = treinadorViewModel.Name;
            pokemonTreinador.PokemonName = pokemonName;

            switch (pokemonTreinador.PokemonIndex)
            {
                case 1:
                    if(treinadorViewModel.Pokemon1Ability != null)
                        pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon1Ability);
                    if(treinadorViewModel.Pokemon1PokemonTypeName1 != null)
                        pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon1PokemonTypeName1);
                    pokemonTreinador.Level = treinadorViewModel.Pokemon1Level;
                    if (!string.IsNullOrEmpty(treinadorViewModel.Pokemon1PokemonTypeName2))
                        pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon1PokemonTypeName2).Id;
                    if(treinadorViewModel.Pokemon1Move1 is not null or "")
                        pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon1Move1).Id;
                    if (treinadorViewModel.Pokemon1Move2 is not null or "")
                        pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon1Move2).Id;
                    if (treinadorViewModel.Pokemon1Move3 is not null or "")
                        pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon1Move3).Id;
                    if (treinadorViewModel.Pokemon1Move4 is not null or "")
                        pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon1Move4).Id;
                    break;
                case 2:
                    if (treinadorViewModel.Pokemon2Ability != null)
                        pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon2Ability);
                    if (treinadorViewModel.Pokemon2PokemonTypeName1 != null)
                        pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon2PokemonTypeName1);
                    pokemonTreinador.Level = treinadorViewModel.Pokemon2Level;
                    if (!string.IsNullOrEmpty(treinadorViewModel.Pokemon2PokemonTypeName2))
                        pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon2PokemonTypeName2).Id;
                    if (treinadorViewModel.Pokemon2Move1 is not null or "")
                        pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon2Move1).Id;
                    if (treinadorViewModel.Pokemon2Move2 is not null or "")
                        pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon2Move2).Id;
                    if (treinadorViewModel.Pokemon2Move3 is not null or "")
                        pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon2Move3).Id;
                    if (treinadorViewModel.Pokemon2Move4 is not null or "")
                        pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon2Move4).Id;
                    break;
                case 3:
                    if (treinadorViewModel.Pokemon3Ability != null)
                        pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon3Ability);
                    if (treinadorViewModel.Pokemon3PokemonTypeName1 != null)
                        pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon3PokemonTypeName1);
                    pokemonTreinador.Level = treinadorViewModel.Pokemon3Level;
                    if (!string.IsNullOrEmpty(treinadorViewModel.Pokemon3PokemonTypeName2))
                        pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon3PokemonTypeName2).Id;
                    if (treinadorViewModel.Pokemon3Move1 is not null or "")
                        pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon3Move1).Id;
                    if (treinadorViewModel.Pokemon3Move2 is not null or "")
                        pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon3Move2).Id;
                    if (treinadorViewModel.Pokemon3Move3 is not null or "")
                        pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon3Move3).Id;
                    if (treinadorViewModel.Pokemon3Move4 is not null or "")
                        pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon3Move4).Id;
                    break;
                case 4:
                    if (treinadorViewModel.Pokemon4Ability != null)
                        pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon4Ability);
                    if (treinadorViewModel.Pokemon4PokemonTypeName1 != null)
                        pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon4PokemonTypeName1);
                    pokemonTreinador.Level = treinadorViewModel.Pokemon4Level;
                    if (!string.IsNullOrEmpty(treinadorViewModel.Pokemon4PokemonTypeName2))
                        pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon4PokemonTypeName2).Id;
                    if (treinadorViewModel.Pokemon4Move1 is not null or "")
                        pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon4Move1).Id;
                    if (treinadorViewModel.Pokemon4Move2 is not null or "")
                        pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon4Move2).Id;
                    if (treinadorViewModel.Pokemon4Move3 is not null or "")
                        pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon4Move3).Id;
                    if (treinadorViewModel.Pokemon4Move4 is not null or "")
                        pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon4Move4).Id;
                    break;
                case 5:
                    if (treinadorViewModel.Pokemon5Ability != null)
                        pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon5Ability);
                    if (treinadorViewModel.Pokemon5PokemonTypeName1 != null)
                        pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon5PokemonTypeName1);
                    pokemonTreinador.Level = treinadorViewModel.Pokemon5Level;
                    if (!string.IsNullOrEmpty(treinadorViewModel.Pokemon5PokemonTypeName2))
                        pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon5PokemonTypeName2).Id;
                    if (treinadorViewModel.Pokemon5Move1 is not null or "")
                        pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon5Move1).Id;
                    if (treinadorViewModel.Pokemon5Move2 is not null or "")
                        pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon5Move2).Id;
                    if (treinadorViewModel.Pokemon5Move3 is not null or "")
                        pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon5Move3).Id;
                    if (treinadorViewModel.Pokemon5Move4 is not null or "")
                        pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon5Move4).Id;
                    break;
                case 6:
                    if (treinadorViewModel.Pokemon6Ability != null)
                        pokemonTreinador.Ability = _context.PokemonAbility.First(e => e.Name == treinadorViewModel.Pokemon6Ability);
                    if (treinadorViewModel.Pokemon6PokemonTypeName1 != null)
                        pokemonTreinador.Type = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon6PokemonTypeName1);
                    pokemonTreinador.Level = treinadorViewModel.Pokemon6Level;
                    if (!string.IsNullOrEmpty(treinadorViewModel.Pokemon6PokemonTypeName2))
                        pokemonTreinador.SecondTypeId = _context.PokemonType.First(e => e.Name == treinadorViewModel.Pokemon6PokemonTypeName2).Id;
                    if (treinadorViewModel.Pokemon6Move1 is not null or "")
                        pokemonTreinador.Move1Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon6Move1).Id;

                    if (treinadorViewModel.Pokemon6Move2 is not null or "")
                        pokemonTreinador.Move2Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon6Move2).Id;
                    if (treinadorViewModel.Pokemon6Move3 is not null or "")
                        pokemonTreinador.Move3Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon6Move3).Id;
                    if (treinadorViewModel.Pokemon6Move4 is not null or "")
                        pokemonTreinador.Move4Id = _context.Move.First(e => e.Name == treinadorViewModel.Pokemon6Move4).Id;
                    break;
                default:
                    break;
            }

            _context.Update(pokemonTreinador);
            _context.SaveChanges();

        }

        public TreinadorViewModel GetTreinadorViewModelData(int treinadorId)
        {
            var treinador = _context.Treinador.First(e => e.Id == treinadorId);
            var listaTipos = _context.PokemonType.ToList();
            var listaAbilidades = _context.PokemonAbility.ToList();
            List<PokemonTreinadorRelacionado> listaPokemonTreinador = _context.PokemonTreinador.Include(e => e.PokemonApiId).Where(e => e.TreinadorId == treinadorId).ToList();
            var treinadorViewModel = new TreinadorViewModel
            {
                Id = treinadorId,
                Name = treinador.Name,
                ImagePath = treinador.ImagePath,
                Location = treinador.Location,
                Pokemons = new List<PokemonDetailsViewModel>()
            };

            foreach (var pokemon in listaPokemonTreinador)
            {
                if (pokemon.PokemonIndex < 1 || pokemon.PokemonIndex > 6)
                    continue; // Ignora índices inválidos

                switch (pokemon.PokemonIndex)
                {
                    case 1:
                        treinadorViewModel.Pokemon1Name = pokemon.PokemonName;
                        treinadorViewModel.Pokemon1Id = pokemon.PokemonApiId.Id;
                        treinadorViewModel.Pokemon1Ability = listaAbilidades.FirstOrDefault(e => e.Id == pokemon.AbilityId)?.Name ?? null;
                        treinadorViewModel.Pokemon1PokemonTypeName1 = listaTipos.First( e => e.Id == pokemon.TypeId)?.Name ?? null;
                        if (pokemon.SecondTypeId != 0)
                            treinadorViewModel.Pokemon1PokemonTypeName2 = listaTipos.First( e => e.Id == pokemon.SecondTypeId)?.Name ?? null;
                        treinadorViewModel.Pokemon1Move1 = GetMoveName(pokemon.Move1Id);
                        treinadorViewModel.Pokemon1Move2 = GetMoveName(pokemon.Move2Id);
                        treinadorViewModel.Pokemon1Move3 = GetMoveName(pokemon.Move3Id);
                        treinadorViewModel.Pokemon1Move4 = GetMoveName(pokemon.Move4Id);
                        treinadorViewModel.Pokemon1Level = pokemon.Level;
                        break;

                    case 2:
                        treinadorViewModel.Pokemon2Name = pokemon.PokemonName;
                        treinadorViewModel.Pokemon2Id = pokemon.PokemonApiId.Id;
                        treinadorViewModel.Pokemon2Ability = listaAbilidades.FirstOrDefault(e => e.Id == pokemon.AbilityId)?.Name ?? null;
                        treinadorViewModel.Pokemon2PokemonTypeName1 = listaTipos.First( e => e.Id == pokemon.TypeId)?.Name ?? null;
                        if(pokemon.SecondTypeId != 0)
                            treinadorViewModel.Pokemon2PokemonTypeName2 = listaTipos.First( e => e.Id == pokemon.SecondTypeId)?.Name ?? null;
                        treinadorViewModel.Pokemon2Move1 = GetMoveName(pokemon.Move1Id);
                        treinadorViewModel.Pokemon2Move2 = GetMoveName(pokemon.Move2Id);
                        treinadorViewModel.Pokemon2Move3 = GetMoveName(pokemon.Move3Id);
                        treinadorViewModel.Pokemon2Move4 = GetMoveName(pokemon.Move4Id);
                        treinadorViewModel.Pokemon2Level = pokemon.Level;
                        break;

                    case 3:
                        treinadorViewModel.Pokemon3Name = pokemon.PokemonName;
                        treinadorViewModel.Pokemon3Id = pokemon.PokemonApiId.Id;
                        treinadorViewModel.Pokemon3Ability = listaAbilidades.FirstOrDefault(e => e.Id == pokemon.AbilityId)?.Name ?? null;
                        treinadorViewModel.Pokemon3PokemonTypeName1 = listaTipos.First( e => e.Id == pokemon.TypeId)?.Name ?? null;
                        if (pokemon.SecondTypeId != 0)
                            treinadorViewModel.Pokemon3PokemonTypeName2 = listaTipos.First( e => e.Id == pokemon.SecondTypeId)?.Name ?? null;
                        treinadorViewModel.Pokemon3Move1 = GetMoveName(pokemon.Move1Id);
                        treinadorViewModel.Pokemon3Move2 = GetMoveName(pokemon.Move2Id);
                        treinadorViewModel.Pokemon3Move3 = GetMoveName(pokemon.Move3Id);
                        treinadorViewModel.Pokemon3Move4 = GetMoveName(pokemon.Move4Id);
                        treinadorViewModel.Pokemon3Level = pokemon.Level;
                        break;

                    case 4:
                        treinadorViewModel.Pokemon4Name = pokemon.PokemonName;
                        treinadorViewModel.Pokemon4Id = pokemon.PokemonApiId.Id;
                        treinadorViewModel.Pokemon4Ability = listaAbilidades.FirstOrDefault(e => e.Id == pokemon.AbilityId)?.Name ?? null;
                        treinadorViewModel.Pokemon4PokemonTypeName1 = listaTipos.First( e => e.Id == pokemon.TypeId)?.Name ?? null;
                        if (pokemon.SecondTypeId != 0)
                            treinadorViewModel.Pokemon4PokemonTypeName2 = listaTipos.First( e => e.Id == pokemon.SecondTypeId)?.Name ?? null;
                        treinadorViewModel.Pokemon4Move1 = GetMoveName(pokemon.Move1Id);
                        treinadorViewModel.Pokemon4Move2 = GetMoveName(pokemon.Move2Id);
                        treinadorViewModel.Pokemon4Move3 = GetMoveName(pokemon.Move3Id);
                        treinadorViewModel.Pokemon4Move4 = GetMoveName(pokemon.Move4Id);
                        treinadorViewModel.Pokemon4Level = pokemon.Level;
                        break;

                    case 5:
                        treinadorViewModel.Pokemon5Name = pokemon.PokemonName;
                        treinadorViewModel.Pokemon5Id = pokemon.PokemonApiId.Id;
                        treinadorViewModel.Pokemon5Ability = listaAbilidades.FirstOrDefault(e => e.Id == pokemon.AbilityId)?.Name ?? null;
                        treinadorViewModel.Pokemon5PokemonTypeName1 = listaTipos.First( e => e.Id == pokemon.TypeId)?.Name ?? null;
                        if (pokemon.SecondTypeId != 0)
                            treinadorViewModel.Pokemon5PokemonTypeName2 = listaTipos.First( e => e.Id == pokemon.SecondTypeId)?.Name ?? null;
                        treinadorViewModel.Pokemon5Move1 = GetMoveName(pokemon.Move1Id);
                        treinadorViewModel.Pokemon5Move2 = GetMoveName(pokemon.Move2Id);
                        treinadorViewModel.Pokemon5Move3 = GetMoveName(pokemon.Move3Id);
                        treinadorViewModel.Pokemon5Move4 = GetMoveName(pokemon.Move4Id);
                        treinadorViewModel.Pokemon5Level = pokemon.Level;
                        break;

                    case 6:
                        treinadorViewModel.Pokemon6Name = pokemon.PokemonName;
                        treinadorViewModel.Pokemon6Id = pokemon.PokemonApiId.Id;
                        treinadorViewModel.Pokemon6Ability = listaAbilidades.FirstOrDefault(e => e.Id == pokemon.AbilityId)?.Name ?? null;
                        treinadorViewModel.Pokemon6PokemonTypeName1 = listaTipos.First( e => e.Id == pokemon.TypeId)?.Name ?? null;
                        if (pokemon.SecondTypeId != 0)
                            treinadorViewModel.Pokemon6PokemonTypeName2 = listaTipos.First( e => e.Id == pokemon.SecondTypeId)?.Name ?? null;
                        treinadorViewModel.Pokemon6Move1 = GetMoveName(pokemon.Move1Id);
                        treinadorViewModel.Pokemon6Move2 = GetMoveName(pokemon.Move2Id);
                        treinadorViewModel.Pokemon6Move3 = GetMoveName(pokemon.Move3Id);
                        treinadorViewModel.Pokemon6Move4 = GetMoveName(pokemon.Move4Id);
                        treinadorViewModel.Pokemon6Level = pokemon.Level;
                        break;
                }
            }

            return treinadorViewModel;
            // Função auxiliar para buscar o nome do movimento pelo ID (pode ser otimizada via cache)
            string GetMoveName(int? moveId)
            {
                if (!moveId.HasValue) return null;
                var move = _context.Move.FirstOrDefault(m => m.Id == moveId);
                return move?.Name ?? "Unknown Move";
            }
        }

        public void DeleteTreinador(int treinadorId)
        {
            var treinador = _context.Treinador.First(e => e.Id == treinadorId);

            var listaTreinadorPokemon = _context.PokemonTreinador
                .Where(e => e.Treinador == treinador)
                .ToList(); // Converte para lista para evitar problemas na remoção

            _context.RemoveRange(listaTreinadorPokemon); // Remove todos os registros relacionados ao treinador
            _context.Remove(treinador); // Remove o treinador

            _context.SaveChanges();
        }

    }
}
