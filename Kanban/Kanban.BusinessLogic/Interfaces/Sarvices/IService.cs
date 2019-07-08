using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanban.BusinessLogic.Interfaces.Sarvices
{
    public interface IService<T> where T: class, DataAccess.Interfaces.IIdentity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        Task DeleteAsync(int id);
    }
}
