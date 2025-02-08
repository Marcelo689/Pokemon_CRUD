using ApiModelsResponse.ViewModels;
using DB.Models;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Service;

namespace WebMVC.Controllers
{
    public class TreinadorController : Controller
    {
        private readonly TreinadorService _treinadorService;
        public TreinadorController(TreinadorService treinadorService)
        {
            _treinadorService = treinadorService;
        }
        private static List<TreinadorViewModel> _treinadores = new List<TreinadorViewModel>(); // Simulação de banco de dados

        public IActionResult Index()
        {
            return View(_treinadores);
        }

        public IActionResult Create()
        {
            var treinadorViewModel = new TreinadorViewModel();
            _treinadorService.FillTreinadorViewModel(treinadorViewModel);
            return View(treinadorViewModel);
        }

        public static Treinador Convert(TreinadorViewModel treinadorViewModel)
        {
            return new Treinador
            {
                Id = treinadorViewModel.Id,
                Name = treinadorViewModel.Name,
                ImagePath = treinadorViewModel.ImagePath,
                Location = treinadorViewModel.Location,
            };
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

                Treinador treinadorModel = Convert(treinador);
                _treinadorService.AddTreinador(treinadorModel);
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