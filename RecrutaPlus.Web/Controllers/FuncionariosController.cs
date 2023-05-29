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
    public class FuncionariosController : BaseController
    {
        private readonly IFuncionarioService _employeeService;

        public FuncionariosController(
            IMapper mapper,
            IAppLogger logger,
            IFuncionarioService employeeService) : base(logger, mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(int? id, bool state = false)
        {
            FuncionarioSearch employeeSearch = new FuncionarioSearch();
            IEnumerable<Funcionario> employees = null;

            if (!state)
            {
                TempData[DefaultConst.TEMPDATA_FILTERSTATE] = null;
            }

            if (!string.IsNullOrWhiteSpace(TempData[DefaultConst.TEMPDATA_FILTERSTATE]?.ToString()))
            {
                employeeSearch = JsonSerializer.Deserialize<FuncionarioSearch>(TempData[DefaultConst.TEMPDATA_FILTERSTATE]?.ToString());
                if (employeeSearch.HasFilter)
                {
                    employees = await _employeeService.GetByTakeLastRelatedAsync(employeeSearch.TakeLast);
                }
                else
                {
                    FuncionarioFilter filter = _mapper.Map<FuncionarioFilterViewModel, FuncionarioFilter>(employeeSearch?.Filter);
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
                    Funcionario employee = await _employeeService.GetByIdRelatedAsync(id.GetValueOrDefault(-1));
                    if (employee != null)
                    {
                        employees = new List<Funcionario>() { employee };
                    }
                }
                else
                {
                    employees = await _employeeService.GetByTakeLastRelatedAsync(employeeSearch.TakeLast);
                }
            }

            List<FuncionarioViewModel> employeeViewModels = _mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(employees).ToList();

            employeeSearch.Itens = employeeViewModels;

            _logger.LogInformation(FuncionarioConst.LOG_INDEX, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return View(employeeSearch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FuncionarioSearch employeeSearch)
        {
            IEnumerable<Funcionario> employees;

            if (employeeSearch.HasFilter)
            {
                employees = await _employeeService.GetByTakeLastRelatedAsync(employeeSearch.TakeLast);
            }
            else
            {
                FuncionarioFilter filter = _mapper.Map<FuncionarioFilterViewModel, FuncionarioFilter>(employeeSearch?.Filter);
                employees = await _employeeService.GetByFilterRelatedAsync(filter);
            }

            List<FuncionarioViewModel> demployeeViewModels = _mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(employees).ToList();

            employeeSearch.Itens = demployeeViewModels;

            TempData[DefaultConst.TEMPDATA_FILTERSTATE] = JsonSerializer.Serialize(employeeSearch, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

            _logger.LogInformation(FuncionarioConst.LOG_INDEX, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return View(employeeSearch);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Funcionario employee = await _employeeService.GetByIdRelatedAsync(id.GetValueOrDefault(-1));

            if (employee == null)
            {
                return NotFound();
            }

            //AutoMapper
            var employeeViewModel = _mapper.Map<Funcionario, FuncionarioViewModel>(employee);

            _logger.LogInformation(FuncionarioConst.LOG_DETAILS, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return View(employeeViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Funcionario employee = await _employeeService.GetByIdRelatedAsync(id.GetValueOrDefault(-1));

            if (employee == null)
            {
                return NotFound();
            }

            //AutoMapperc
            var employeeViewModel = _mapper.Map<Funcionario, FuncionarioViewModel>(employee);

            _logger.LogInformation(FuncionarioConst.LOG_EDIT, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FuncionarioViewModel employeeViewModel)
        {
            if (id != employeeViewModel.FuncionarioId)
            {
                return NotFound();
            }

            //AutoMapper
            var employee = _mapper.Map<FuncionarioViewModel, Funcionario>(employeeViewModel);

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

            _logger.LogInformation(FuncionarioConst.LOG_EDIT, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return RedirectToAction(nameof(Index), new { id = employee?.FuncionarioId });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Funcionario employee = await _employeeService.GetByIdRelatedAsync(id.GetValueOrDefault(-1));

            if (employee == null)
            {
                return NotFound();
            }
            //AutoMapper
            var employeeViewModel = _mapper.Map<Funcionario, FuncionarioViewModel>(employee);

            _logger.LogInformation(FuncionarioConst.LOG_DELETE, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

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
                return RedirectToAction(nameof(Delete), new { id = employee?.FuncionarioId });
            }

            _ = await _employeeService.SaveChangesAsync();

            SuccessMessage = DefaultResource.MSG_SAVED_SUCCESSFULLY;

            _logger.LogInformation(FuncionarioConst.LOG_DELETE, User.Identity.Name ?? DefaultConst.USER_ANONYMOUS, DateTime.Now);

            return RedirectToAction(nameof(Index));
        }

    }
}