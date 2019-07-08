namespace Kanban.BusinessLogic.DTOs
{
    public class TaskTypeDto : DataAccess.Interfaces.IIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
