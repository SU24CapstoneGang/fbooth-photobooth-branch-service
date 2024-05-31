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
            builder.Property(r => r.RoleID).HasColumnName("RoleID")
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

            //Add primordial data
            builder.HasData(
                new Role
                {
                    RoleID = new Guid("4a1f17f8-c66f-4c6e-a0c2-8b95b8d4e5bb"),
                    RoleName = "Admin"
                },
                new Role
                {
                    RoleID = new Guid("9d7c0f8b-cb6f-4894-8e91-b5b594c1c37c"),
                    RoleName = "Customer"
                },
                new Role
                {
                    RoleID = new Guid("e82f0f61-4488-4b18-bf68-540704917e6a"),
                    RoleName = "BranchManager"
                },
                new Role
                {
                    RoleID = new Guid("2f3d2f4b-8370-4b12-8e5c-1e38c3f9bace"),
                    RoleName = "Manager"
                });
        }
    }
}
