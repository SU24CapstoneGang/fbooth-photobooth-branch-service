using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BoothBranchConfigurations : IEntityTypeConfiguration<BoothBranch>
    {
        public void Configure(EntityTypeBuilder<BoothBranch> builder)
        {
            // Table name
            builder.ToTable("BoothBranches");

            // Primary key
            builder.HasKey(pb => pb.BoothBranchID);
            builder.Property(pb => pb.BoothBranchID).HasColumnName("BoothBranchID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(pb => pb.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pb => pb.BranchAddress)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(pb => pb.CreateDate)
            .IsRequired();

            // ManufactureStatus enum mapping
            builder.Property(pb => pb.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ManufactureStatus)Enum.Parse(typeof(ManufactureStatus), v));

            // Relationship with Account - manager
            builder.HasOne(pb => pb.Manager)
                .WithOne(a => a.BoothBranchManage)
                .HasForeignKey<BoothBranch>(pb => pb.ManagerID)
                .IsRequired(false);
            // Relationship with Account - staff
            builder.HasMany(pb => pb.Staffs)
                .WithOne(a => a.BoothBranchBelong)
                .HasForeignKey(a => a.PhotoBoothBranchID)
                .IsRequired(false);
            // Relationship with Session
            builder.HasMany(pb => pb.SessionOrders)
                .WithOne(s => s.BoothBranch)
                .HasForeignKey(s => s.BoothBranchID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(b => b.Booths)
                .WithOne(a => a.PhotoBoothBranch)
                .HasForeignKey(a => a.PhotoBoothBranchID)
                .IsRequired();
        }
    }
}
