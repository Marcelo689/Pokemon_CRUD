using ApiModelsResponse.ViewModels;
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
            var treinadorModel = new Treinador
            {
                Id = treinadorViewModel.Id,
                Name = treinadorViewModel.Name,
                ImagePath = treinadorViewModel.ImagePath,
                Location = treinadorViewModel.Location,
            };

            // Iterando sobre os 6 Pokémon
            for (int i = 1; i <= 6; i++)
            {
                // Acessando as propriedades do treinadorViewModel
                var pokemonName = treinadorViewModel.GetType().GetProperty($"Pokemon{i}Name")?.GetValue(treinadorViewModel) as string;

                if (!string.IsNullOrEmpty(pokemonName))
                {
                    var pokemon = new PokemonTreinadorRelacionado
                    {
                        Level = (int)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Level")?.GetValue(treinadorViewModel),
                        Treinador = treinadorModel,
                        Move1Id = _treinadorService.GetMoveByName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Move1")?.GetValue(treinadorViewModel))?.Id ?? 0,
                        Move2Id = _treinadorService.GetMoveByName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Move2")?.GetValue(treinadorViewModel))?.Id ?? 0,
                        Move3Id = _treinadorService.GetMoveByName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Move3")?.GetValue(treinadorViewModel))?.Id ?? 0,
                        Move4Id = _treinadorService.GetMoveByName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Move4")?.GetValue(treinadorViewModel))?.Id ?? 0,
                        Name = pokemonName,
                        Ability = _treinadorService.GetAbilityFromName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}Ability")?.GetValue(treinadorViewModel)),
                        PokemonApiId = _treinadorService.GetPokemonFromName(pokemonName),
                        Type = _treinadorService.GetPokemonTypeFromName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}PokemonTypeName1")?.GetValue(treinadorViewModel)),
                        SecondTypeId = _treinadorService.GetPokemonTypeFromName((string)treinadorViewModel.GetType().GetProperty($"Pokemon{i}PokemonTypeName2")?.GetValue(treinadorViewModel)).Id,
                    };

                    // Adiciona o Pokémon ao treinador
                    _treinadorService.AddTreinadorAndPokemon(treinadorModel, pokemon);
                }
            }
        }


        [HttpPost]
        public IActionResult Create(TreinadorViewModel treinador, IFormFile image)
        {
            if (treinador.IsValid())
            {
                if(image is not null && image.Length > 0)
                    AdicionaImagem(treinador, image);
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
    }
}