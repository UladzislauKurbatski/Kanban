using Kanban.DataAccess.Entities;
using System.Threading.Tasks;

namespace Kanban.DataAccess.Interfaces.Repositories
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        Task<TaskEntity> TransactionUpdateAsync(TaskEntity entity);
    }
}
