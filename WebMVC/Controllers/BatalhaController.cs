using Microsoft.AspNetCore.Mvc;
using ModelsResponse.ViewModels;
using Service.Service;

namespace WebMVC.Controllers
{
    public class BatalhaController : Controller
    {
        readonly private BatalhaService _batalhaService;
        public BatalhaController(BatalhaService batalhaService)
        {
            _batalhaService = batalhaService;
        }
        public IActionResult Index()
        {
            var treinadores = _batalhaService.GetAllTrainers();
            var batalhaViewModel = new BatalhaViewModel();
            batalhaViewModel.TreinadoresViewModel = treinadores;
            return View(batalhaViewModel);
        }
        public IActionResult Battle(int trainer1, int trainer2)
        {
            var batalhaViewModel = new BatalhaViewModel();
            batalhaViewModel.PokemonsTrainer1 = _batalhaService.GetPokemonsFromTrainerId(trainer1);
            batalhaViewModel.PokemonsTrainer2 = _batalhaService.GetPokemonsFromTrainerId(trainer2);
            return View(batalhaViewModel);
        }
    }
}
