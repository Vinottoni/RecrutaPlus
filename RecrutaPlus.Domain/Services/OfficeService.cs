using RecrutaPlus.Application.ViewModels;
using RecrutaPlus.Domain.Constants;
using RecrutaPlus.Domain.Entities;
using RecrutaPlus.Domain.Interfaces.Repositories;
using RecrutaPlus.Domain.Interfaces.Services;
using RecrutaPlus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecrutaPlus.Domain.Services
{
    public class OfficeService : Service<Office>, IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IAppLogger _logger;


        public OfficeService(IOfficeRepository parametroRepository, IAppLogger logger)
            : base(parametroRepository)
        {
            _officeRepository = parametroRepository;
            _logger = logger;
        }

        public override ServiceResult Add(Office entity)
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

            _logger.LogInformation(OfficeConst.LOG_TABLE_ADD, DateTime.Now, entity.GuidStamp, entity.cargoId, entity);

            return serviceResult;
        }

        public override ServiceResult Update(Office entity)
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

            _logger.LogInformation(OfficeConst.LOG_TABLE_UPDATE, DateTime.Now, entity.GuidStamp, entity.cargoId, entity);

            return serviceResult;
        }

        public override ServiceResult Delete(Office entity)
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

            _logger.LogInformation(OfficeConst.LOG_TABLE_REMOVE, DateTime.Now, entity.GuidStamp, entity.cargoId, entity);

            return serviceResult;
        }
        public async Task<Office> GetByIdAsync(int id)
        {
            return await _officeRepository.GetByIdAsync(id);
        }
        //public async Task<Office> GetByIdRelatedAsync(int id)
        //{
        //    return await _officeRepository.GetByIdRelatedAsync(id);
        //}
        public async Task<IEnumerable<Office>> GetByFilterAsync(OfficeFilter filter = null)
        {
            return await _officeRepository.GetByFilterAsync(filter);
        }

        public async Task<IEnumerable<Office>> GetByPageAsync(int skip, int take, Expression<Func<Office, bool>> predicate = null)
        {
            return await _officeRepository.GetByPageAsync(skip, take, predicate);
        }

        public async Task<IEnumerable<Office>> GetByTakeLastAsync(int takeLast, Expression<Func<Office, bool>> predicate = null)
        {
            return await _officeRepository.GetByTakeLastAsync(takeLast, predicate);
        }

        //public async Task<IEnumerable<Office>> GetByQueryRelatedAsync(Expression<Func<Office, bool>> predicate = null)
        //{
        //    return await _officeRepository.GetByQueryRelatedAsync(predicate);
        //}

        //public async Task<IEnumerable<Office>> GetByFilterRelatedAsync(OfficeFilter filter = null)
        //{
        //    return await _officeRepository.GetByFilterRelatedAsync(filter);
        //}

        //public async Task<IEnumerable<Office>> GetByPageRelatedAsync(int skip, int take, Expression<Func<Office, bool>> predicate = null)
        //{
        //    return await _officeRepository.GetByPageRelatedAsync(skip, take, predicate);
        //}

        //public async Task<IEnumerable<Office>> GetByTakeLastRelatedAsync(int takeLast, Expression<Func<Office, bool>> predicate = null)
        //{
        //    return await _officeRepository.GetByTakeLastRelatedAsync(takeLast, predicate);
        //}
    }
}
