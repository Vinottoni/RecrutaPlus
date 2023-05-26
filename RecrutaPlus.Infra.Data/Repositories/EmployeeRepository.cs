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
            return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(s => s.funcionarioId == id);
        }

        public async Task<Employee> GetByIdRelatedAsync(int id)
        {
            return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .Include(i => i.Login)
                .SingleOrDefaultAsync(s => s.funcionarioId == id);
        }

        public async Task<IEnumerable<Employee>> GetByFilterAsync(EmployeeFilter filter = null)
        {
            var _query = _dbContext.Employees.AsNoTrackingWithIdentityResolution();

            if (filter?.funcionarioId != null) { _query = _query.Where(w => w.funcionarioId == filter.funcionarioId.GetValueOrDefault());}
            if (filter?.cargoId != null) { _query = _query.Where(w => w.cargoId == filter.cargoId); }
            if (filter?.nome != null) { _query = _query.Where(w => w.nome == filter.nome); }
            if (filter?.rg != null) { _query = _query.Where(w => w.rg == filter.rg); }
            if (filter?.cpf != null) { _query = _query.Where(w => w.cpf == filter.cpf); }
            if (filter?.email != null) { _query = _query.Where(w => w.email == filter.email); }
            if (filter?.telefone != null) { _query = _query.Where(w => w.telefone == filter.telefone); }
            if (filter?.dataNascimento != null) { _query = _query.Where(w => w.dataNascimento == filter.dataNascimento); }
            if (filter?.genero != null) { _query = _query.Where(w => w.genero == filter.genero); }
            if (filter?.cep != null) { _query = _query.Where(w => w.cep == filter.cep); }
            if (filter?.endereco != null) { _query = _query.Where(w => w.endereco == filter.endereco); }
            if (filter?.bairro != null) { _query = _query.Where(w => w.bairro == filter.bairro); }
            if (filter?.educacao != null) { _query = _query.Where(w => w.educacao == filter.educacao); }
            if (filter?.status != null) { _query = _query.Where(w => w.status == filter.status); }

            return await _query.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByFilterRelatedAsync(EmployeeFilter filter = null)
        {
            var _query = _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .Include(i => i.Login);

            //if (filter?.funcionarioId != null) { _query = _query.Where(w => w.funcionarioId == filter.funcionarioId.GetValueOrDefault()); }
            //if (filter?.cargoId != null) { _query = _query.Where(w => w.cargoId == filter.cargoId); }
            //if (filter?.nome != null) { _query = _query.Where(w => w.nome == filter.nome); }
            //if (filter?.rg != null) { _query = _query.Where(w => w.rg == filter.rg); }
            //if (filter?.cpf != null) { _query = _query.Where(w => w.cpf == filter.cpf); }
            //if (filter?.email != null) { _query = _query.Where(w => w.email == filter.email); }
            //if (filter?.telefone != null) { _query = _query.Where(w => w.telefone == filter.telefone); }
            //if (filter?.dataNascimento != null) { _query = _query.Where(w => w.dataNascimento == filter.dataNascimento); }
            //if (filter?.genero != null) { _query = _query.Where(w => w.genero == filter.genero); }
            //if (filter?.cep != null) { _query = _query.Where(w => w.cep == filter.cep); }
            //if (filter?.endereco != null) { _query = _query.Where(w => w.endereco == filter.endereco); }
            //if (filter?.bairro != null) { _query = _query.Where(w => w.bairro == filter.bairro); }
            //if (filter?.educacao != null) { _query = _query.Where(w => w.educacao == filter.educacao); }
            //if (filter?.status != null) { _query = _query.Where(w => w.status == filter.status); }

            ////Office
            //if (filter?.OfficeFilter?.nome != null) { _query = _query.Where(w => w.Office.nome == filter.OfficeFilter.nome); }
            //if (filter?.OfficeFilter?.descricao != null) { _query = _query.Where(w => w.Office.descricao == filter.OfficeFilter.descricao); }
            //if (filter?.OfficeFilter?.salario != null) { _query = _query.Where(w => w.Office.salario == filter.OfficeFilter.salario); }

            ////Login
            //if (filter?.LoginFilter?.username != null) { _query = _query.Where(w => w.Login.username == filter.LoginFilter.username); }

            return await _query.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByPageAsync(int skip, int take, Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().OrderBy(o => o.funcionarioId).Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().Where(predicate).OrderBy(o => o.funcionarioId).Skip(skip).Take(take).ToListAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByQueryRelatedAsync(Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                    .Include(i => i.Office)
                    .Include(i => i.Login)
                    .ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                    .Include(i => i.Office)
                    .Include(i => i.Login)
                    .Where(predicate).ToListAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByTakeLastAsync(int takeLast, Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().OrderBy(o => o.funcionarioId).Take(takeLast).ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution().Where(predicate).OrderBy(o => o.funcionarioId).Take(takeLast).ToListAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByPageRelatedAsync(int skip, int take, Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .Include(i => i.Login)
                .OrderBy(o => o.funcionarioId).Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .Include(i => i.Login)
                .Where(predicate).OrderBy(o => o.funcionarioId).Skip(skip).Take(take).ToListAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByTakeLastRelatedAsync(int takeLast, Expression<Func<Employee, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .Include(i => i.Login)
                .OrderBy(o => o.funcionarioId).Take(takeLast).ToListAsync();
            }
            else
            {
                return await _dbContext.Employees.AsNoTrackingWithIdentityResolution()
                .Include(i => i.Office)
                .Include(i => i.Login)
                .Where(predicate).OrderBy(o => o.funcionarioId).Take(takeLast).ToListAsync();
            }
        }

    }
}
