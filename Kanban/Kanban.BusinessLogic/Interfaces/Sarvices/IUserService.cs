using Kanban.BusinessLogic.DTOs;

namespace Kanban.BusinessLogic.Interfaces.Sarvices
{
    public interface IUserService : IService<UserDto>
    {
        bool CheckIfLoginExists(string login, int? id = null);

        UserDto AuthenticateUser(string login, string password);
    }
}
