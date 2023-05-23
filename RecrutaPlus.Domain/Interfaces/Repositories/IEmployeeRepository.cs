using RecrutaPlus.Application.ViewModels;
using RecrutaPlus.Domain.Entities;
using System.Linq.Expressions;

namespace RecrutaPlus.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepositoryAsync<Employee>
    {
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> GetByIdRelatedAsync(int id);
        Task<IEnumerable<Employee>> GetByFilterAsync(EmployeeFilter filter = null);
        //Task<IEnumerable<Employee>> GetByFilterRelatedAsync(EmployeeFilter filter = null);
        Task<IEnumerable<Employee>> GetByPageAsync(int skip, int take, Expression<Func<Employee, bool>> predicate = null);
        Task<IEnumerable<Employee>> GetByPageRelatedAsync(int skip, int take, Expression<Func<Employee, bool>> predicate = null);
        Task<IEnumerable<Employee>> GetByTakeLastAsync(int takeLast, Expression<Func<Employee, bool>> predicate = null);
        Task<IEnumerable<Employee>> GetByTakeLastRelatedAsync(int takeLast, Expression<Func<Employee, bool>> predicate = null);
        Task<IEnumerable<Employee>> GetByQueryRelatedAsync(Expression<Func<Employee, bool>> predicate = null);
    }
}
