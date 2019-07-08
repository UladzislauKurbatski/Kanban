using Kanban.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kanban.DataAccess.EntityConfiguration
{
    class TaskStatusEntityConfiguration : IEntityTypeConfiguration<TaskStatusEntity>
    {
        public void Configure(EntityTypeBuilder<TaskStatusEntity> builder)
        {
            builder.ToTable("TaskStatuses");
            builder.HasKey(t => t.Id);

            builder
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
