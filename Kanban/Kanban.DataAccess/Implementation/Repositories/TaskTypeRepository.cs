using Kanban.DataAccess.Entities;
using Kanban.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kanban.DataAccess.Implementation.Repositories
{
    public class TaskTypeRepository : EFRepository<TaskTypeEntity>, ITaskTypeRepository
    {
        public TaskTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
