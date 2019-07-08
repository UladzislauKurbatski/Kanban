using Kanban.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.DataAccess.EntityConfiguration
{
    class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(t => t.Id);

            builder
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            builder
                .Property(t => t.CreationDate)
                .IsRequired();

            builder
                .HasOne<TaskEntity>()
                .WithMany()
                .HasForeignKey(t => t.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<TaskStatusEntity>()
                .WithMany()
                .HasForeignKey(t => t.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<TaskTypeEntity>()
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<UserEntity>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
