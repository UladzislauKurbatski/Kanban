using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kanban.DataAccess.Migrations
{
    public partial class Databaseinitialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ToDo" },
                    { 2, "InProgress" },
                    { 3, "Hold" }
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Task" },
                    { 2, "Sub-task" },
                    { 3, "Bug" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "bydny", "Vlad", "kanban19", "Admin" },
                    { 2, "ukos", "John", "kanban2019", "Manager" },
                    { 3, "razor", "Till", "2019kanban", "User" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreationDate", "Description", "ParentId", "StatusId", "Title", "TypeId", "UserId" },
                values: new object[] { 1, new DateTime(2019, 7, 6, 18, 1, 0, 854, DateTimeKind.Utc).AddTicks(9355), "Investigate solutions for project architecture", null, 2, "Investigate ", 1, null });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreationDate", "Description", "ParentId", "StatusId", "Title", "TypeId", "UserId" },
                values: new object[] { 2, new DateTime(2019, 7, 6, 18, 1, 0, 855, DateTimeKind.Utc).AddTicks(571), "Implement Data Access layer", null, 2, "Implement DAL", 1, 3 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreationDate", "Description", "ParentId", "StatusId", "Title", "TypeId", "UserId" },
                values: new object[] { 3, new DateTime(2019, 7, 6, 18, 2, 0, 855, DateTimeKind.Utc).AddTicks(1772), "Implement main entities and configure models using fluent api", 2, 2, "Implement entities", 2, 3 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreationDate", "Description", "ParentId", "StatusId", "Title", "TypeId", "UserId" },
                values: new object[] { 4, new DateTime(2019, 7, 6, 18, 31, 0, 855, DateTimeKind.Utc).AddTicks(1794), "Implement abstractions of repositories", 2, 1, "Implement interfaces", 2, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
