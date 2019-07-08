using Kanban.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.DataAccess.EntityConfiguration
{
    class TaskTypeEntityConfiguration : IEntityTypeConfiguration<TaskTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TaskTypeEntity> builder)
        {
            builder.ToTable("TaskTypes");
            builder.HasKey(t => t.Id);

            builder
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
