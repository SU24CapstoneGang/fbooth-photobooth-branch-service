using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration;

public class PhotoBoothBranchesConfiguration : IEntityTypeConfiguration<PhotoBoothBranches>
{
    public void Configure(EntityTypeBuilder<PhotoBoothBranches> builder)
    {
        builder.ToTable("PhotoBoothBranches");

        builder.HasKey(p => p.Id);
        builder.Property(u => u.Id).HasColumnName("PhotoBoothBranch ID")
            .ValueGeneratedNever();
        builder.Property(p => p.BranchName).HasMaxLength(255).IsRequired();
        builder.Property(p => p.BranchAddress).HasMaxLength(255).IsRequired();

        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<int>(); // Convert enum to int for storage

        // Configure relationship
        builder.HasOne(a => a.Account)
               .WithOne()
               .HasForeignKey<PhotoBoothBranches>(p => p.AccountId)
               .IsRequired(false);
        builder.HasOne(p => p.Camera)
               .WithOne()
               .HasForeignKey<PhotoBoothBranches>(c => c.CameraId)
               .IsRequired(false);
        builder.HasOne(p => p.Printer)
               .WithOne()
               .HasForeignKey<PhotoBoothBranches>(p => p.PrinterId)
               .IsRequired(false);
    }
}
