﻿// <auto-generated />
using System;
using Kanban.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kanban.DataAccess.Migrations
{
    [DbContext(typeof(KanbanDbContext))]
    [Migration("20190706183101_Database initialization")]
    partial class Databaseinitialization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kanban.DataAccess.Entities.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<int?>("ParentId");

                    b.Property<int>("StatusId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("TypeId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2019, 7, 6, 18, 1, 0, 854, DateTimeKind.Utc).AddTicks(9355),
                            Description = "Investigate solutions for project architecture",
                            StatusId = 2,
                            Title = "Investigate ",
                            TypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2019, 7, 6, 18, 1, 0, 855, DateTimeKind.Utc).AddTicks(571),
                            Description = "Implement Data Access layer",
                            StatusId = 2,
                            Title = "Implement DAL",
                            TypeId = 1,
                            UserId = 3
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2019, 7, 6, 18, 2, 0, 855, DateTimeKind.Utc).AddTicks(1772),
                            Description = "Implement main entities and configure models using fluent api",
                            ParentId = 2,
                            StatusId = 2,
                            Title = "Implement entities",
                            TypeId = 2,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            CreationDate = new DateTime(2019, 7, 6, 18, 31, 0, 855, DateTimeKind.Utc).AddTicks(1794),
                            Description = "Implement abstractions of repositories",
                            ParentId = 2,
                            StatusId = 1,
                            Title = "Implement interfaces",
                            TypeId = 2
                        });
                });

            modelBuilder.Entity("Kanban.DataAccess.Entities.TaskStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TaskStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ToDo"
                        },
                        new
                        {
                            Id = 2,
                            Name = "InProgress"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Hold"
                        });
                });

            modelBuilder.Entity("Kanban.DataAccess.Entities.TaskTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TaskTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Task"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sub-task"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bug"
                        });
                });

            modelBuilder.Entity("Kanban.DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "bydny",
                            Name = "Vlad",
                            Password = "kanban19",
                            Role = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Login = "ukos",
                            Name = "John",
                            Password = "kanban2019",
                            Role = "Manager"
                        },
                        new
                        {
                            Id = 3,
                            Login = "razor",
                            Name = "Till",
                            Password = "2019kanban",
                            Role = "User"
                        });
                });

            modelBuilder.Entity("Kanban.DataAccess.Entities.TaskEntity", b =>
                {
                    b.HasOne("Kanban.DataAccess.Entities.TaskEntity")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Kanban.DataAccess.Entities.TaskStatusEntity")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Kanban.DataAccess.Entities.TaskTypeEntity")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Kanban.DataAccess.Entities.UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}