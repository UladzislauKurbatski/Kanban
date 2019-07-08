using Kanban.BusinessLogic.DTOs;
using System.Threading.Tasks;

namespace Kanban.BusinessLogic.Interfaces.Sarvices
{
    public interface ITaskService : IService<TaskDto>
    {
        bool CheckIfTitleExists(string name, int? id = null);
        Task<TaskDto> AttachToUserAsync(int taskId, int userId);
    }
}
