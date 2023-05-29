using RecrutaPlus.Application.ViewModels;
using RecrutaPlus.Domain.Constants;
using RecrutaPlus.Domain.Entities;
using RecrutaPlus.Domain.Interfaces;
using RecrutaPlus.Domain.Interfaces.Repositories;
using RecrutaPlus.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace RecrutaPlus.Domain.Services
{
    public class EmployeeService : Service<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppLogger _logger;


        public EmployeeService(IEmployeeRepository parametroRepository, IAppLogger logger)
            : base(parametroRepository)
        {
            _employeeRepository = parametroRepository;
            _logger = logger;
        }

        public override ServiceResult Add(Employee entity)
        {
            ServiceResult serviceResult = new ServiceResult();

            if (!entity.IsValid())
            {
                foreach (var error in entity.ValidationResult.Errors)
                {
                    serviceResult.AddError(error.PropertyName, error.ErrorMessage);
                }
                return serviceResult;
            }

            if (serviceResult.HasErrors)
            {
                return serviceResult;
            }

            base.Add(entity);

            _logger.LogInformation(EmployeeConst.LOG_TABLE_ADD, DateTime.Now, entity.GuidStamp, entity.FuncionarioId, entity);

            return serviceResult;
        }

        public override ServiceResult Update(Employee entity)
        {
            ServiceResult serviceResult = new ServiceResult();

            if (!entity.IsValid())
            {
                foreach (var error in entity.ValidationResult.Errors)
                {
                    serviceResult.AddError(error.PropertyName, error.ErrorMessage);
                }
                return serviceResult;
            }

            if (serviceResult.HasErrors)
            {
                return serviceResult;
            }

            base.Update(entity);

            _logger.LogInformation(EmployeeConst.LOG_TABLE_UPDATE, DateTime.Now, entity.GuidStamp, entity.FuncionarioId, entity);

            return serviceResult;
        }

        public override ServiceResult Delete(Employee entity)
        {
            ServiceResult serviceResult = new ServiceResult();

            if (!entity.IsValid())
            {
                foreach (var error in entity.ValidationResult.Errors)
                {
                    serviceResult.AddError(error.PropertyName, error.ErrorMessage);
                }
                return serviceResult;
            }


            if (serviceResult.HasErrors)
            {
                return serviceResult;
            }

            base.Delete(entity);

            _logger.LogInformation(EmployeeConst.LOG_TABLE_REMOVE, DateTime.Now, entity.GuidStamp, entity.FuncionarioId, entity);

            return serviceResult;
        }
        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }
        public async Task<Employee> GetByIdRelatedAsync(int id)
        {
            return await _employeeRepository.GetByIdRelatedAsync(id);
        }
        public async Task<IEnumerable<Employee>> GetByFilterAsync(EmployeeFilter filter = null)
        {
            return await _employeeRepository.GetByFilterAsync(filter);
        }

        public async Task<IEnumerable<Employee>> GetByPageAsync(int skip, int take, Expression<Func<Employee, bool>> predicate = null)
        {
            return await _employeeRepository.GetByPageAsync(skip, take, predicate);
        }

        public async Task<IEnumerable<Employee>> GetByTakeLastAsync(int takeLast, Expression<Func<Employee, bool>> predicate = null)
        {
            return await _employeeRepository.GetByTakeLastAsync(takeLast, predicate);
        }

        public async Task<IEnumerable<Employee>> GetByQueryRelatedAsync(Expression<Func<Employee, bool>> predicate = null)
        {
            return await _employeeRepository.GetByQueryRelatedAsync(predicate);
        }

        public async Task<IEnumerable<Employee>> GetByFilterRelatedAsync(EmployeeFilter filter = null)
        {
            return await _employeeRepository.GetByFilterRelatedAsync(filter);
        }

        public async Task<IEnumerable<Employee>> GetByPageRelatedAsync(int skip, int take, Expression<Func<Employee, bool>> predicate = null)
        {
            return await _employeeRepository.GetByPageRelatedAsync(skip, take, predicate);
        }

        public async Task<IEnumerable<Employee>> GetByTakeLastRelatedAsync(int takeLast, Expression<Func<Employee, bool>> predicate = null)
        {
            return await _employeeRepository.GetByTakeLastRelatedAsync(takeLast, predicate);
        }
    }
}
