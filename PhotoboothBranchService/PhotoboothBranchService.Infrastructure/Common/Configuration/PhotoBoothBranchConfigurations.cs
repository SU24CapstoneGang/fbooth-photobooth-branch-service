using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PhotoBoothBranchConfigurations : IEntityTypeConfiguration<PhotoBoothBranch>
    {
        public void Configure(EntityTypeBuilder<PhotoBoothBranch> builder)
        {
            // Table name
            builder.ToTable("PhotoBoothBranches");

            // Primary key
            builder.HasKey(pb => pb.PhotoBoothBranchID);
            builder.Property(pb => pb.PhotoBoothBranchID).HasColumnName("PhotoBoothBranchID")
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

            // Relationship with Account
            builder.HasOne(pb => pb.Account)
                .WithMany(a => a.PhotoBoothBranches)
                .HasForeignKey(pb => pb.AccountID)
                .IsRequired();

            // Relationship with Session
            builder.HasMany(pb => pb.SessionOrders)
                .WithOne(s => s.PhotoBoothBranch)
                .HasForeignKey(s => s.PhotoBoothBranchID)
                .IsRequired();

            builder.HasMany(b => b.Booths)
                .WithOne(a => a.PhotoBoothBranch)
                .HasForeignKey(a => a.PhotoBoothBranchID)
                .IsRequired();
        }
    }
}
