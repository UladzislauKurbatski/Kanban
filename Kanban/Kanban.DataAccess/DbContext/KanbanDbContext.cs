using Kanban.DataAccess.Entities;
using Kanban.DataAccess.EntityConfiguration;
using Kanban.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.DataAccess.DatabaseContext
{
    public class KanbanDbContext : DbContext
    {
        public KanbanDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskStatusEntity> TaskTypes { get; set; }
        public DbSet<TaskTypeEntity> TaskStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskStatusEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData
            (
                new UserEntity { Id = 1, Login = "bydny", Name = "Vlad", Password = "kanban19", Role = Roles.Admin.ToString() },
                new UserEntity { Id = 2, Login = "ukos", Name = "John", Password = "kanban2019", Role = Roles.Manager.ToString() },
                new UserEntity { Id = 3, Login = "razor", Name = "Till", Password = "2019kanban", Role = Roles.User.ToString() }
            );

            modelBuilder.Entity<TaskTypeEntity>().HasData
            (
                new TaskTypeEntity { Id = 1, Name = "Task" },
                new TaskTypeEntity { Id = 2, Name = "Sub-task" },
                new TaskTypeEntity { Id = 3, Name = "Bug" }
            );

            modelBuilder.Entity<TaskStatusEntity>().HasData
            (
                new TaskStatusEntity { Id = 1, Name = "ToDo" },
                new TaskStatusEntity { Id = 2, Name = "InProgress" },
                new TaskStatusEntity { Id = 3, Name = "Hold" }
            );

            modelBuilder.Entity<TaskEntity>().HasData
            (
                new TaskEntity
                {
                    Id = 1,
                    Title = "Investigate ",
                    Description = "Investigate solutions for project architecture",
                    CreationDate = DateTime.UtcNow.AddHours(-0.5),
                    TypeId = 1,
                    StatusId = 2,
                },
                new TaskEntity
                {
                    Id = 2,
                    Title = "Implement DAL",
                    Description = "Implement Data Access layer",
                    CreationDate = DateTime.UtcNow.AddMinutes(-30),
                    TypeId = 1,
                    StatusId = 2,
                    UserId = 3,
                },
                new TaskEntity
                {
                    Id = 3,
                    Title = "Implement entities",
                    Description = "Implement main entities and configure models using fluent api",
                    ParentId = 2,
                    CreationDate = DateTime.UtcNow.AddMinutes(-29),
                    TypeId = 2,
                    StatusId = 2,
                    UserId = 3,
                },
                new TaskEntity
                {
                    Id = 4,
                    Title = "Implement interfaces",
                    Description = "Implement abstractions of repositories",
                    ParentId = 2,
                    CreationDate = DateTime.UtcNow,
                    TypeId = 2,
                    StatusId = 1
                }
            );
        }
    }

    
}
