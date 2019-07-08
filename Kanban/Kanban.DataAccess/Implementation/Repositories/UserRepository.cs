using Kanban.DataAccess.Entities;
using Kanban.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kanban.DataAccess.Implementation.Repositories
{
    public class UserRepository : EFRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
