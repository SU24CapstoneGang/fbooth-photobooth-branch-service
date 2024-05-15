using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration
{
    public class PhotoBoothBranchConfigurations : IEntityTypeConfiguration<PhotoBoothBranch>
    {
        public void Configure(EntityTypeBuilder<PhotoBoothBranch> builder)
        {
            // Table name
            builder.ToTable("PhotoBoothBranches");

            // Primary key
            builder.HasKey(pb => pb.BranchesID);
            builder.Property(pb => pb.BranchesID).HasColumnName("Branches ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(pb => pb.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pb => pb.BranchAddress)
                .IsRequired()
                .HasMaxLength(255);

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

            // Relationship with Camera
            builder.HasOne(pb => pb.Camera)
                .WithOne(c => c.PhotoBoothBranch)
                .HasForeignKey<Camera>(c => c.PhotoBoothBranchId)
                .IsRequired(false);

            // Relationship with Printer
            builder.HasOne(pb => pb.Printer)
                .WithOne(p => p.PhotoBoothBranch)
                .HasForeignKey<Printer>(p => p.PhotoBoothBranchId)
                .IsRequired(false);

            // Relationship with Session
            builder.HasMany(pb => pb.Sessions)
                .WithOne(s => s.PhotoBoothBranch)
                .HasForeignKey(s => s.BranchesID)
                .IsRequired();
        }
    }
}
