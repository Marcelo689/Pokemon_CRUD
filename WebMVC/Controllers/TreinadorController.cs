﻿using ApiModelsResponse.ViewModels;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Service;

namespace WebMVC.Controllers
{
    public class TreinadorController : Controller
    {
        public readonly TreinadorService _treinadorService;
        public TreinadorController(TreinadorService treinadorService)
        {
            _treinadorService = treinadorService;
        }
        private static List<TreinadorViewModel> _treinadores = new List<TreinadorViewModel>(); // Simulação de banco de dados

        public IActionResult Index()
        {
            _treinadores = _treinadorService.GetAllTrainers();
            return View(_treinadores);
        }

        public IActionResult Create()
        {
            var treinadorViewModel = new TreinadorViewModel();
            _treinadorService.FillTreinadorViewModel(treinadorViewModel);
            return View(treinadorViewModel);
        }
        public void SalvaTreinadorAndPokemons(TreinadorViewModel treinadorViewModel)
        {
            try
            {

                Treinador treinadorModel = null;
                bool isEdit = treinadorViewModel.Id != 0;
                if (isEdit)
                {//Edit
                    treinadorModel = _treinadorService.GetTreinadorById(treinadorViewModel.Id);
                }
                else
                {//Create
                    treinadorModel = new Treinador
                    {
                        Name = treinadorViewModel.Name,
                        ImagePath = treinadorViewModel.ImagePath,
                        Location = treinadorViewModel.Location,
                    };
                }

                for (int i = 1; i <= 6; i++)
                {
                    var pokemonName = treinadorViewModel.GetType().GetProperty($"Pokemon{i}Name")?.GetValue(treinadorViewModel) as string;

                    bool hasName = !string.IsNullOrEmpty(pokemonName);
                    if (hasName)
                    {
                        var pokemon = _treinadorService.GeTreinadorPokemonFromName(treinadorModel, i);
                        if (isEdit && pokemon is not null)
                        {
                            _treinadorService.UpdatePokemon(pokemon, pokemonName, treinadorViewModel);
                        }
                        else
                        {
                            pokemon = new PokemonTreinadorRelacionado();
                            pokemon.PokemonIndex = i;
                            pokemon.Level = (int)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Level")?.GetValue(treinadorViewModel);
                            pokemon.Treinador = treinadorModel;
                            pokemon.Move1Id = _treinadorService.GetMoveByName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Move1")?.GetValue(treinadorViewModel))?.Id ?? 0;
                            pokemon.Move2Id = _treinadorService.GetMoveByName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Move2")?.GetValue(treinadorViewModel))?.Id ?? 0;
                            pokemon.Move3Id = _treinadorService.GetMoveByName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Move3")?.GetValue(treinadorViewModel))?.Id ?? 0;
                            pokemon.Move4Id = _treinadorService.GetMoveByName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Move4")?.GetValue(treinadorViewModel))?.Id ?? 0;
                            pokemon.PokemonName = pokemonName;
                            pokemon.Ability = _treinadorService.GetAbilityFromName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Ability")?.GetValue(treinadorViewModel));
                            pokemon.PokemonApiId = _treinadorService.GetPokemonFromName(pokemonName);
                            pokemon.Type = _treinadorService.GetPokemonTypeFromName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}PokemonTypeName1")?.GetValue(treinadorViewModel));
                            pokemon.SecondTypeId = _treinadorService.GetPokemonTypeFromName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}PokemonTypeName2")?.GetValue(treinadorViewModel))?.Id ?? 0;
                            pokemon.TreinadorName = treinadorModel.Name;

                            // Adiciona o Pokémon ao treinador
                            _treinadorService.AddTreinadorAndPokemon(treinadorModel, pokemon, isEdit, i);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(TreinadorViewModel treinador, IFormFile imagePath, IFormFile imageUpload)
        {
            if (treinador.IsValid())
            {
                if (imagePath is not null && imagePath.Length > 0)
                    AdicionaImagem(treinador, imagePath);
                else
                    treinador.ImagePath = "/images/default.png";
                SalvaTreinadorAndPokemons(treinador);
                return RedirectToAction("Index");
            }
            return View(treinador);
        }

        private static void AdicionaImagem(TreinadorViewModel treinador, IFormFile image)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            bool directoryDosentExist = !Directory.Exists(uploadFolder);
            if (directoryDosentExist)
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            treinador.ImagePath = "/upload/" + uniqueFileName;
        }

        public IActionResult Edit(TreinadorViewModel treinadorViewModel)
        {
            treinadorViewModel = _treinadorService.GetTreinadorPokemonsData(treinadorViewModel);
            treinadorViewModel = _treinadorService.FillTreinadorViewModel(treinadorViewModel);
            return View(treinadorViewModel);
        }


        [HttpPost]
        public IActionResult Edit(TreinadorViewModel treinador, IFormFile imagePath, IFormFile imageUpload)
        {
            if (treinador.IsValid())
            {
                if (imagePath is not null && imagePath.Length > 0)
                    AdicionaImagem(treinador, imagePath);
                else
                    treinador.ImagePath = "/images/default.png";
                SalvaTreinadorAndPokemons(treinador);
                return RedirectToAction("Index");
            }
            return View(treinador);
        }

    }
}