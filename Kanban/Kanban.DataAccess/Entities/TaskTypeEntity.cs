using System.Collections.Generic;

namespace Kanban.DataAccess.Entities
{
    public class TaskTypeEntity : BaseEntity, Interfaces.IIdentity
    {
        public string Name { get; set; }
    }
}
