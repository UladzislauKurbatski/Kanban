namespace Kanban.BusinessLogic.DTOs
{
    public class UserDto : DataAccess.Interfaces.IIdentity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
