using System.Collections.Generic;

namespace Kanban.DataAccess.Entities
{
    public class UserEntity : BaseEntity, Interfaces.IIdentity
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
