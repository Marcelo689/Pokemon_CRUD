using ApiModelsResponse.ApiModels;
using ApiModelsResponse.ApiModels.ApiResponse;
using DB.Data;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace WebMVC.Data
{
    public static class DbInitializer
    {
        const string Physical = "physical";
        const string Status = "status";
        const string Special = "special";
        public static async Task CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<PokemonContext>();

                if (context.Pokemon.Any())
                    return;

                var httpClient = services.GetRequiredService<HttpClient>();
                httpClient.Timeout = TimeSpan.FromSeconds(60);

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PokemonStatsDetails ON");

                await DbInitializer.SavePokemonsFromApi(context, httpClient);
                await DbInitializer.SaveTypesFromApi(context, httpClient);
                await DbInitializer.SaveAbilitiesFromApi(context, httpClient);
                await DbInitializer.SaveMovesFromApi(context, httpClient);


                await context.SaveChangesAsync();
                await DbInitializer.SavePokemonDetails(context, httpClient);
                await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PokemonStatsDetails OFF");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static async Task SavePokemonDetails(PokemonContext context, HttpClient httpClient)
        {
            var pokemons = context.Pokemon.ToList();
            var allPokemonTypes = context.PokemonType.ToList(); // Pré-carrega tipos no cliente
            var allAbilities = context.PokemonAbility.ToList(); // Pré-carrega habilidades no cliente

            try
            {
                foreach (var pokemon in pokemons)
                {
                    string url = $"https://pokeapi.co/api/v2/pokemon/{pokemon.Name.ToLower()}";

                    var response = await httpClient.GetStringAsync(url);
                    var pokemonDetails = JsonSerializer.Deserialize<PokemonDetailsResponse>(response);

                    var entidade = new PokemonDetails
                    {
                        PokemonId = pokemon.Id,
                        Name = pokemon.Name,
                        ImageUrl = pokemonDetails.ImageUrl,
                        HP = pokemonDetails.Stats[0].BaseStat,
                        ATTACK = pokemonDetails.Stats[1].BaseStat,
                        DEFENSE = pokemonDetails.Stats[2].BaseStat,
                        SP_ATTACK = pokemonDetails.Stats[3].BaseStat,
                        SP_DEFENSE = pokemonDetails.Stats[4].BaseStat,
                        SPEED = pokemonDetails.Stats[5].BaseStat,
                        height = pokemonDetails.height,
                        weight = pokemonDetails.weight,
                        PokemonType1 = allPokemonTypes.FirstOrDefault(e => pokemonDetails.Types.Any(f => f.Type.Name == e.Name)),
                        PokemonType2 = pokemonDetails.Types.Count > 1
                            ? allPokemonTypes.FirstOrDefault(e => pokemonDetails.Types[1].Type.Name == e.Name)
                            : null,
                    };

                    entidade.PokemonTypeId1 = entidade.PokemonType1 != null ? entidade.PokemonType1.Id : (int?)null;
                    entidade.PokemonTypeId2 = entidade.PokemonType2 != null ? entidade.PokemonType2.Id : (int?)null;

                    for (var i = 0; i < pokemonDetails.Abilities.Count; i++)
                    {
                        var ability = pokemonDetails.Abilities[i];
                        string abilityName = ability.Ability.Name;

                        var abilityDb = allAbilities.FirstOrDefault(e => e.Name == abilityName);
                        if (abilityDb != null)
                        {
                            switch (i)
                            {
                                case 0:
                                    entidade.Ability1 = abilityDb;
                                    entidade.Ability1Id = abilityDb.Id;
                                    break;
                                case 1:
                                    entidade.Ability2 = abilityDb;
                                    entidade.Ability2Id = abilityDb.Id;
                                    break;
                                case 2:
                                    entidade.Ability3 = abilityDb;
                                    entidade.Ability3Id = abilityDb.Id;
                                    break;
                            }
                        }
                    }

                    context.PokemonStatsDetails.Add(entidade);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static async Task SaveTypesFromApi(PokemonContext context, HttpClient httpClient)
        {
            string url = "https://pokeapi.co/api/v2/type"; // Busca todos os tipos
            try
            {
                var response = await httpClient.GetStringAsync(url);
                var typeList = JsonSerializer.Deserialize<TypeListResponse>(response);

                foreach (var type in typeList.TypeListResponseProp)
                {
                    var newType = new DB.Models.PokemonType
                    {
                        Name = type.Name,
                        Url = type.Url,
                    };

                    if (!context.PokemonType.Any(t => t.Name == newType.Name))
                    {
                        context.PokemonType.Add(newType);
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("A requisição foi cancelada: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro inesperado: " + ex.Message);
            }
        }

        private static async Task SaveMovesFromApi(PokemonContext context, HttpClient httpClient)
        {
            try
            {
                string url = "https://pokeapi.co/api/v2/move?limit=1000"; // Puxa todos os movimentos
                var response = await httpClient.GetStringAsync(url);
                var movesList = JsonSerializer.Deserialize<MoveListReponse>(response);

                foreach (MoveViewModelResponse move in movesList.Moves)
                {
                    var moveDetailsResponse = await httpClient.GetStringAsync(move.Url);
                    var moveDetails = JsonSerializer.Deserialize<MoveDetailsResponse>(moveDetailsResponse);

                    var newMove = new Move
                    {
                        Name = move.Name,
                        Accuracy = moveDetails.Accuracy,
                        Power = moveDetails.Power,
                        PP = moveDetails.PP,
                        Description = moveDetails.EffectEntryList.FirstOrDefault()?.Effect ?? "No Description available",
                        ShortDescription = moveDetails.EffectEntryList.FirstOrDefault()?.ShortEffect ?? "No Description available",
                        isPhysical = moveDetails.DamageClass.Name == Physical,
                        isSpecial = moveDetails.DamageClass.Name == Special,
                        isStatus = moveDetails.DamageClass.Name == Status,
                    };

                    foreach (var pokemon in moveDetails.LearnedByPokemonList)
                    {
                        context.PokemonMove.Add(new PokemonMove
                        {
                            PokemonName = pokemon.Name,
                            MoveName = move.Name,
                            MoveId = moveDetails.Id,
                            Power = moveDetails.Power,
                            Accuracy = moveDetails.Accuracy,
                            PP = moveDetails.PP,
                            IsSpecial = moveDetails.DamageClass.Name == Special,
                            isPhysical = moveDetails.DamageClass.Name == Physical,
                            isStatus = moveDetails.DamageClass.Name == Status,
                            ShortDescription = moveDetails.EffectEntryList.FirstOrDefault()?.ShortEffect ?? "No Description available",
                        });
                    }

                    if (!context.Move.Any(m => m.Name == newMove.Name))
                    {
                        context.Move.Add(newMove);
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("A requisição foi cancelada: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro inesperado: " + ex.Message);
            }

        }

        private static async Task SaveAbilitiesFromApi(PokemonContext context, HttpClient httpClient)
        {
            try
            {

                string url = "https://pokeapi.co/api/v2/ability?limit=1000"; // Puxa todas as habilidades
                var response = await httpClient.GetStringAsync(url);
                var abilitiesList = JsonSerializer.Deserialize<AbilityListResponse>(response);

                foreach (var ability in abilitiesList.Results)
                {
                    var abilityDetailsResponse = await httpClient.GetStringAsync(ability.Url);
                    var abilityDetails = JsonSerializer.Deserialize<AbilityDetailsResponse>(abilityDetailsResponse);

                    var newAbilityEntity = new DB.Models.PokemonAbility
                    {
                        Name = abilityDetails.Name,
                        Description = abilityDetails.EffectEntries.Last()?.Effect ?? "Sem descrição"
                    };

                    if (!context.PokemonAbility.Any(a => a.Name == newAbilityEntity.Name))
                    {
                        context.PokemonAbility.Add(newAbilityEntity);
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("A requisição foi cancelada: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro inesperado: " + ex.Message);
            }

        }

        private static async Task SavePokemonsFromApi(PokemonContext context, HttpClient httpClient)
        {
            try
            {
                int limit = 1000;
                string url = $"https://pokeapi.co/api/v2/pokemon?limit={limit}";
                var response = await httpClient.GetStringAsync(url);
                var pokemonList = JsonSerializer.Deserialize<PokemonListResponse>(response);

                var pokemons = new List<PokemonViewModelResponse>();

                foreach (var pokemon in pokemonList.Results)
                {
                    var responseDetails = await httpClient.GetStringAsync(pokemon.Url);
                    var detail = JsonSerializer.Deserialize<PokemonDetailSprites>(responseDetails);

                    var pokemonEntity = new Pokemon
                    {
                        Name = pokemon.Name,
                        Url = detail.Sprites.Other.OfficialArtwork.FrontDefault
                    };

                    context.Pokemon.Add(pokemonEntity);
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("A requisição foi cancelada: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro inesperado: " + ex.Message);
            }

        }

    }
}
