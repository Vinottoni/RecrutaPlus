using RecrutaPlus.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RecrutaPlus.Domain.Interfaces.Services;
using AutoMapper;
using RecrutaPlus.Domain.Interfaces;
using RecrutaPlus.Domain.Entities;
using RecrutaPlus.Domain.Constants;
using System.Text.Json.Serialization;
using System.Text.Json;
using RecrutaPlus.Application.Searches;
using RecrutaPlus.Application.Filters;
using RecrutaPlus.Domain.Services;
using RecrutaPlus.Domain.Resources;
using RecrutaPlus.Web.Extensions;

namespace RecrutaPlus.Web.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
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
                    employees = await _employeeService.GetByTakeLastRelatedAsync(employeeSearch.TakeLast);
                }
            }

            List<EmployeeViewModel> employeeViewModels = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees).ToList();

            employeeSearch.Itens = employeeViewModels;

            _logger.LogInformation(EmployeeConst.LOG_INDEX, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

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

            List<EmployeeViewModel> demployeeViewModels = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees).ToList();

            employeeSearch.Itens = demployeeViewModels;

            TempData[DefaultConst.TEMPDATA_FILTERSTATE] = JsonSerializer.Serialize(employeeSearch, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

            _logger.LogInformation(EmployeeConst.LOG_INDEX, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return View(employeeSearch);
        }

        public async Task<IActionResult> Details(int? id)
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
            var employeeViewModel = _mapper.Map<Employee, EmployeeViewModel>(employee);

            _logger.LogInformation(EmployeeConst.LOG_DETAILS, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return View(employeeViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
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

            //AutoMapperc
            var employeeViewModel = _mapper.Map<Employee, EmployeeViewModel>(employee);

            _logger.LogInformation(EmployeeConst.LOG_EDIT, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.funcionarioId)
            {
                return NotFound();
            }

            //AutoMapper
            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);

            employee.Edicao = DateTime.Now;
            employee.EditadoPor = User.Identity.Name ?? DefaultConst.USER_ANONYMOUS;

            ServiceResult serviceResult = _employeeService.Update(employee);

            //Validation
            if (serviceResult.HasErrors)
            {
                //ViewBag.SelectListColaboradores = await Task.Run(() => SelectListColaboradores());

                serviceResult.ToModelStateDictionary(ModelState);
                return View(employeeViewModel);
            }

            _ = await _employeeService.SaveChangesAsync();

            SuccessMessage = DefaultResource.MSG_UPDATE_SUCCESSFULLY;

            _logger.LogInformation(EmployeeConst.LOG_EDIT, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return RedirectToAction(nameof(Index), new { id = employee?.funcionarioId });
        }

        public async Task<IActionResult> Delete(int? id)
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
            var employeeViewModel = _mapper.Map<Employee, EmployeeViewModel>(employee);

            _logger.LogInformation(EmployeeConst.LOG_DELETE, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return View(employeeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeeService.FindByIdAsync(id);
            ServiceResult serviceResult = _employeeService.Delete(employee);

            //Validation
            if (serviceResult.HasErrors)
            {
                serviceResult.ToModelStateDictionary(ModelState);
                ErrorMessage = serviceResult.ToHtml();
                return RedirectToAction(nameof(Delete), new { id = employee?.funcionarioId });
            }

            _ = await _employeeService.SaveChangesAsync();

            SuccessMessage = DefaultResource.MSG_SAVED_SUCCESSFULLY;

            _logger.LogInformation(EmployeeConst.LOG_DELETE, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return RedirectToAction(nameof(Index));
        }

    }
}