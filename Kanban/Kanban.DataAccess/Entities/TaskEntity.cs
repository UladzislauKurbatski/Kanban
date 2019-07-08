using System;
using System.Collections.Generic;

namespace Kanban.DataAccess.Entities
{
    public class TaskEntity : BaseEntity, Interfaces.IIdentity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int? UserId { get; set; }
        public int? ParentId { get; set; }
    }
}
