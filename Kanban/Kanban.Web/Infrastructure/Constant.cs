namespace Kanban.Api.Infrastructure
{
    public class Constant
    {
        public const string CONFIG_SECTION_KANBAN_DB_CONNECTION_STRING = "KanbanConnection";
        public const string CONFIG_SECTION_KANBAN_TOKEN_MANAGEMENT = "TokenManagement";

        internal static class Roles
        {
            public const string Admin = "Admin";
            public const string Manager = "Manager";
            public const string User = "User";
        }
    }
}
