using RecrutaPlus.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RecrutaPlus.Web.Models;
using System.Diagnostics;

namespace RecrutaPlus.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            OfficeViewModel profileViewModel = new OfficeViewModel();


            return View(profileViewModel);
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