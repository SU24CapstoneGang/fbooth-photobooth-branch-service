using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BranchConfigurations : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            // Table name
            builder.ToTable("Branches");

            // Primary key
            builder.HasKey(pb => pb.BranchID);
            builder.Property(pb => pb.BranchID).HasColumnName("BranchID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(pb => pb.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pb => pb.Address)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(pb => pb.Town).IsRequired();
            builder.Property(pb => pb.City).IsRequired();

            builder.Property(pb => pb.CreateDate)
            .IsRequired();

            builder.Property(s => s.CreateDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("GETDATE()")
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // ManufactureStatus enum mapping
            builder.Property(pb => pb.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (BranchStatus)Enum.Parse(typeof(BranchStatus), v));

            // Relationship with Account - manager
            builder.HasOne(pb => pb.Manager)
                .WithOne(a => a.BranchManage)
                .HasForeignKey<Branch>(pb => pb.ManagerID)
                .IsRequired(false);
            // Relationship with Account - staff
            builder.HasMany(pb => pb.Staffs)
                .WithOne(a => a.BranchBelong)
                .HasForeignKey(a => a.BranchID)
                .IsRequired(false);

            builder.HasMany(b => b.Booths)
                .WithOne(a => a.Branch)
                .HasForeignKey(a => a.BranchID)
                .IsRequired();

        
        }
    }
}
