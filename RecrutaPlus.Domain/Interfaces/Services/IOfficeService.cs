using RecrutaPlus.Application.ViewModels;
using RecrutaPlus.Domain.Entities;
using System.Linq.Expressions;

namespace RecrutaPlus.Domain.Interfaces.Services
{
    public interface IOfficeService: IService<Office>
    {
        Task<Office> GetByIdAsync(int id);
        //Task<Office> GetByIdRelatedAsync(int id);
        Task<IEnumerable<Office>> GetByFilterAsync(OfficeFilter filter = null);
        //Task<IEnumerable<Office>> GetByFilterRelatedAsync(OfficeFilter filter = null);
        Task<IEnumerable<Office>> GetByPageAsync(int skip, int take, Expression<Func<Office, bool>> predicate = null);
        //Task<IEnumerable<Office>> GetByPageRelatedAsync(int skip, int take, Expression<Func<Office, bool>> predicate = null);
        Task<IEnumerable<Office>> GetByTakeLastAsync(int takeLast, Expression<Func<Office, bool>> predicate = null);
        //Task<IEnumerable<Office>> GetByTakeLastRelatedAsync(int takeLast, Expression<Func<Office, bool>> predicate = null);
    }
}
