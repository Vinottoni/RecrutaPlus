using AutoMapper;
using RecrutaPlus.Application.Filters;
using RecrutaPlus.Application.Searches;
using RecrutaPlus.Application.ViewModels;
using RecrutaPlus.Domain.Constants;
using RecrutaPlus.Domain.Entities;
//using RecrutaPlus.Domain.Filters;
using RecrutaPlus.Domain.Interfaces;
using RecrutaPlus.Domain.Interfaces.Services;
using RecrutaPlus.Domain.Resources;
using RecrutaPlus.Domain.Services;
using RecrutaPlus.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace RecrutaPlus.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger _logger;
        private readonly IEmployeeService _employeeService;

        public RegisterController(IMapper mapper, IAppLogger logger, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _logger = logger;
            _employeeService = employeeService;
        }
        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string SuccessMessage { get; set; }

        public IActionResult Index()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();


            return View(employeeViewModel);
        }

        //public IActionResult Create()
        //{
        //    RegisterViewModel registerViewModel = new RegisterViewModel();


        //    return View(registerViewModel);

        //}
        public async Task<IActionResult> Create()
        {
            ViewBag.SelectListCargos = await Task.Run(() => SelectListCargos());

            _logger.LogInformation(EmployeeConst.LOG_CREATE, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            EmployeeViewModel employeeViewModel = await Task.Run(() => new EmployeeViewModel());

            return View(employeeViewModel);
        }

        [Authorize(Policy = AuthorizationPolicyConst.REGISTER_CREATE)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            ViewBag.SelectListCargos = await Task.Run(() => SelectListCargos());

            //AutoMapper
            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);

            employee.Cadastro = DateTime.Now;
            employee.CadastradoPor = User.Identity.Name ?? DefaultConst.USER_ANONYMOUS;
            employee.Edicao = DateTime.Now;
            employee.EditadoPor = User.Identity.Name ?? DefaultConst.USER_ANONYMOUS;
            employee.GuidStamp = Guid.NewGuid();

            ServiceResult serviceResult = _employeeService.Add(employee);

            //Validation
            if (serviceResult.HasErrors)
            {

                serviceResult.ToModelStateDictionary(ModelState);

                return View(employeeViewModel);
            }

            _ = await _employeeService.SaveChangesAsync();

            SuccessMessage = EmployeeResource.MSG_SAVED_SUCCESSFULLY;

            _logger.LogInformation(EmployeeConst.LOG_CREATE, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return RedirectToAction(nameof(Index), new { id = employee?.funcionarioId });
        }

        public IActionResult Privacy()
        {
            return View();
        }


        #region SelectList

        private List<SelectListItem> SelectListCargos()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<Employee> employees = _employeeService.GetAllAsync().GetAwaiter().GetResult();

            selectListItems.Add(new SelectListItem(DefaultResource.MSG_SELECIONE, string.Empty, true));

            //foreach (var item in employees.OrderBy(o => o.cargo))
            //{
            //    selectListItems.Add(new SelectListItem(text: item.Nome, value: item.BancoId.ToString()));
            //}

            return selectListItems;
        }

        #endregion
    }
}