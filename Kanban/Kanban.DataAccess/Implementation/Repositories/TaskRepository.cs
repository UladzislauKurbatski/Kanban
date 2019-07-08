using System;
using System.Data;
using System.Threading.Tasks;
using Kanban.DataAccess.Entities;
using Kanban.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kanban.DataAccess.Implementation.Repositories
{
    public class TaskRepository : EFRepository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(DbContext context) : base(context)
        {
        }
        
        public async Task<TaskEntity> TransactionUpdateAsync(TaskEntity entity)
        {
            using (var transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    var updatedEntity = await UpdateAsync(entity);
                    transaction.Commit();
                    return updatedEntity;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
