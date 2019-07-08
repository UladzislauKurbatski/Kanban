using Kanban.BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanban.BusinessLogic.Interfaces.Sarvices
{
    public interface ITaskTypeService : IService<TaskTypeDto>
    {
        bool CheckIfNameExists(string name, int? id = null);
    }
}
