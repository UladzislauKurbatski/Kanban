using Kanban.DataAccess.Entities;
using Kanban.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kanban.DataAccess.Implementation.Repositories
{
    public class TaskStatusRepository : EFRepository<TaskStatusEntity>, ITaskStatusRepository
    {
        public TaskStatusRepository(DbContext context) : base(context)
        {
        }
    }
}
