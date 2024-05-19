using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table name
            builder.ToTable("Roles");

            // Primary key
            builder.HasKey(r => r.RoleID);
            builder.Property(r => r.RoleID).HasColumnName("Role ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            // Relationship with Account
            builder.HasMany(r => r.Accounts)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleID)
                .IsRequired();
        }
    }
}
