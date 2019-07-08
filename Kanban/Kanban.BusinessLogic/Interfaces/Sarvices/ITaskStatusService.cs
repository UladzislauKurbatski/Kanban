using Kanban.BusinessLogic.DTOs;

namespace Kanban.BusinessLogic.Interfaces.Sarvices
{
    public interface ITaskStatusService : IService<TaskStatusDto>
    {
        bool CheckIfNameExists(string name, int? id = null);
    }
}
