using RecrutaPlus.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RecrutaPlus.Web.Models;
using System.Diagnostics;

namespace RecrutaPlus.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();


            return View(employeeViewModel);
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