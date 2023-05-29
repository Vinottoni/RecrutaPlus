using Microsoft.EntityFrameworkCore;
using RecrutaPlus.Application.ViewModels;
using RecrutaPlus.Domain.Entities;
using RecrutaPlus.Domain.Interfaces.Repositories;
using RecrutaPlus.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace RecrutaPlus.Infra.Data.Repositories
{
    public class EmployeeRepository : RepositoryAsync<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext) 
        { 

        }
        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(s => s.FuncionarioId == id);
        }

        public async Task<Employee> GetByIdRelatedAsync(int id)
        {
            return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .SingleOrDefaultAsync(s => s.FuncionarioId == id);
        }

        public async Task<IEnumerable<Employee>> GetByFilterAsync(EmployeeFilter filter = null)
        {
            var _query = _dbContext.Employees.AsNoTrackingWithIdentityResolution();

            if (filter?.FuncionarioId != null) { _query = _query.Where(w => w.FuncionarioId == filter.FuncionarioId.GetValueOrDefault());}
            if (filter?.CargoId != null) { _query = _query.Where(w => w.CargoId == filter.CargoId); }
            if (filter?.Nome != null) { _query = _query.Where(w => w.Nome == filter.Nome); }
            if (filter?.CPF != null) { _query = _query.Where(w => w.CPF == filter.CPF); }
            if (filter?.Status != null) { _query = _query.Where(w => w.Status == filter.Status); }

            return await _query.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByFilterRelatedAsync(EmployeeFilter filter = null)
        {
            var _query = _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office);

            //if (filter?.funcionarioId != null) { _query = _query.Where(w => w.funcionarioId == filter.funcionarioId.GetValueOrDefault()); }
            //if (filter?.cargoId != null) { _query = _query.Where(w => w.cargoId == filter.cargoId); }
            //if (filter?.nome != null) { _query = _query.Where(w => w.nome == filter.nome); }
            //if (filter?.cpf != null) { _query = _query.Where(w => w.cpf == filter.cpf); }
            //if (filter?.Status != null) { _query = _query.Where(w => w.cpf == filter.Status); }

            ////Office
            //if (filter?.OfficeFilter?.nome != null) { _query = _query.Where(w => w.Office.nome == filter.OfficeFilter.nome); }
            //if (filter?.OfficeFilter?.descricao != null) { _query = _query.Where(w => w.Office.descricao == filter.OfficeFilter.descricao); }

            return await _query.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByPageAsync(int skip, int take, Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().OrderBy(o => o.FuncionarioId).Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().Where(predicate).OrderBy(o => o.FuncionarioId).Skip(skip).Take(take).ToListAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByQueryRelatedAsync(Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                    .Include(i => i.Office)
                    .ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                    .Include(i => i.Office)
                    .Where(predicate).ToListAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByTakeLastAsync(int takeLast, Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().OrderBy(o => o.FuncionarioId).Take(takeLast).ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().Where(predicate).OrderBy(o => o.FuncionarioId).Take(takeLast).ToListAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByPageRelatedAsync(int skip, int take, Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .OrderBy(o => o.FuncionarioId).Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .Where(predicate).OrderBy(o => o.FuncionarioId).Skip(skip).Take(take).ToListAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByTakeLastRelatedAsync(int takeLast, Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .OrderBy(o => o.FuncionarioId).Take(takeLast).ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .Where(predicate).OrderBy(o => o.FuncionarioId).Take(takeLast).ToListAsync();
            }
        }

    }
}
