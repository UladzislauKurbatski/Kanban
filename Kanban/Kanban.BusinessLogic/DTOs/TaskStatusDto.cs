namespace Kanban.BusinessLogic.DTOs
{
    public class TaskStatusDto : DataAccess.Interfaces.IIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
