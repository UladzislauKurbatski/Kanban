using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.DataAccess.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, Interfaces.IIdentity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(int id);
        IQueryable<T> Query();
    }
}
