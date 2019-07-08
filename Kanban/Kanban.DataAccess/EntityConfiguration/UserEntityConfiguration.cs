using Kanban.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanban.DataAccess.EntityConfiguration
{
    class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Login)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired(false);

            builder
                .Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(u => u.Role)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
