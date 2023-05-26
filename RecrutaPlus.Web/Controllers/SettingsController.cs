using RecrutaPlus.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RecrutaPlus.Web.Models;
using System.Diagnostics;
using AutoMapper;
using RecrutaPlus.Domain.Interfaces.Services;
using RecrutaPlus.Domain.Interfaces;

namespace RecrutaPlus.Web.Controllers
{
    public class SettingsController : BaseController
    {
        //private readonly ILogger<SettingsController> _logger;

        public SettingsController(
            IMapper mapper,
            IAppLogger logger) : base(logger, mapper)
        {
            _mapper = mapper;
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