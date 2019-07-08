using System;

namespace Kanban.Api.Models.Task
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int? UserId { get; set; }
        public int? ParentId { get; set; }
    }
}
