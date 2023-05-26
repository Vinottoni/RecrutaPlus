using RecrutaPlus.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RecrutaPlus.Web.Models;
using System.Diagnostics;
using AutoMapper;
using RecrutaPlus.Domain.Interfaces.Services;
using RecrutaPlus.Domain.Interfaces;
using RecrutaPlus.Domain.Entities;
using RecrutaPlus.Domain.Constants;

namespace RecrutaPlus.Web.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public SettingsController(
            IMapper mapper,
            IAppLogger logger,
            IEmployeeService employeeService) : base(logger, mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            Employee employee = await _employeeService.GetByIdRelatedAsync(id.GetValueOrDefault(-1));

            if (employee == null)
            {
                return NotFound();
            }

            //AutoMapper
            EmployeeViewModel employeeViewModel = _mapper.Map<Employee, EmployeeViewModel>(employee);

            _logger.LogInformation(EmployeeConst.LOG_INDEX, GetUserName(), DateTime.Now);

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