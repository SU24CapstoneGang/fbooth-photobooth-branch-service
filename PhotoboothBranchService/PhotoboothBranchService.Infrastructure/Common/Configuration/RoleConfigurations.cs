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
            //builder.HasData(
            //    new Role
            //    {
            //        RoleID = new Guid("ba132a6b - 5963 - 4869 - 94d5 - f8736273fe7b"),
            //        RoleName = "Admin"
            //    },
            //    new Role
            //    { 
            //        RoleID = new Guid("f74311a5 - b646 - 4db8 - 9814 - e2154b3f402e"),
            //        RoleName = "Customer"
            //    },
            //    new Role
            //    { 
            //        RoleID = new Guid("9bd86f81-ce71-4a5c-98f0-702ac0409470"),
            //        RoleName = "BranchManager"
            //    },
            //    new Role
            //    {
            //        RoleID = new Guid("80624419-a203-40b7-9dc0-51205b34e1c6"),
            //        RoleName = "Manager"
            //    });
        }
    }
}
