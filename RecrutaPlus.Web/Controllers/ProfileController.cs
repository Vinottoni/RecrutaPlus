using RecrutaPlus.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RecrutaPlus.Web.Models;
using System.Diagnostics;
using RecrutaPlus.Domain.Interfaces.Services;
using AutoMapper;
using RecrutaPlus.Domain.Interfaces;
using RecrutaPlus.Domain.Constants;
using RecrutaPlus.Domain.Entities;

namespace RecrutaPlus.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public ProfileController(
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
            if (id == null)
            {
                return NotFound();
            }

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
    }
}