using System.Collections.Generic;

namespace Kanban.DataAccess.Entities
{
    public class TaskStatusEntity : BaseEntity, Interfaces.IIdentity
    {
        public string Name { get; set; }
    }
}
