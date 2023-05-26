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
    public class RegisterController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public RegisterController(
            IMapper mapper, 
            IAppLogger logger, 
            IEmployeeService employeeService) : base(logger, mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(int? id, bool state = false)
        {
            EmployeeSearch employeeSearch = new EmployeeSearch();
            IEnumerable<Employee> employees = null;

            if (!state)
            {
                TempData[DefaultConst.TEMPDATA_FILTERSTATE] = null;
            }

            if (!string.IsNullOrWhiteSpace(TempData[DefaultConst.TEMPDATA_FILTERSTATE]?.ToString()))
            {
                employeeSearch = JsonSerializer.Deserialize<EmployeeSearch>(TempData[DefaultConst.TEMPDATA_FILTERSTATE]?.ToString());
                if (employeeSearch.HasFilter)
                {
                    employees = await _employeeService.GetByTakeLastRelatedAsync(employeeSearch.TakeLast);
                }
                else
                {
                    EmployeeFilter filter = _mapper.Map<EmployeeFilterViewModel, EmployeeFilter>(employeeSearch?.Filter);
                    employees = await _employeeService.GetByFilterRelatedAsync(filter);
                }

                if (state)
                {
                    TempData[DefaultConst.TEMPDATA_FILTERSTATE] = JsonSerializer.Serialize(employeeSearch, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
                }
            }
            else
            {
                if (id != null)
                {
                    Employee employee = await _employeeService.GetByIdRelatedAsync(id.GetValueOrDefault(-1));
                    if (employee != null)
                    {
                        employees = new List<Employee>() { employee };
                    }
                }
                else
                {
                    employeeSearch.TakeLast = int.MaxValue;
                    employees = await _employeeService.GetByTakeLastRelatedAsync(employeeSearch.TakeLast);
                }
            }

            List<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees).ToList();

            employeeSearch.Itens = employeeViewModels;

            EmployeeViewModel employeeViewModel = new EmployeeViewModel();

            _logger.LogInformation(EmployeeConst.LOG_INDEX, GetUserName(), DateTime.Now);

            return View(employeeSearch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EmployeeSearch employeeSearch)
        {
            IEnumerable<Employee> employees;

            if (employeeSearch.HasFilter)
            {
                employees = await _employeeService.GetByTakeLastRelatedAsync(employeeSearch.TakeLast);
            }
            else
            {
                EmployeeFilter filter = _mapper.Map<EmployeeFilterViewModel, EmployeeFilter>(employeeSearch?.Filter);
                employees = await _employeeService.GetByFilterRelatedAsync(filter);
            }

            List<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees).ToList();

            employeeSearch.Itens = employeeViewModels;

            TempData[DefaultConst.TEMPDATA_FILTERSTATE] = JsonSerializer.Serialize(employeeSearch, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

            _logger.LogInformation(EmployeeConst.LOG_INDEX, GetUserName(), DateTime.Now);

            return View(employeeSearch);
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