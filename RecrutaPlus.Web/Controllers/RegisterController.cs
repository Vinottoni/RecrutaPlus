using RecrutaPlus.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RecrutaPlus.Web.Models;
using System.Diagnostics;

namespace RecrutaPlus.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();


            return View(registerViewModel);
        }

        public IActionResult Create()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();


            return View(registerViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}