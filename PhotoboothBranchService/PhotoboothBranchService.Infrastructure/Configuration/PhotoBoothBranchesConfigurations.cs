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

        builder.Property(p => p.BranchName).HasMaxLength(255).IsRequired();
        builder.Property(p => p.BranchAddress).HasMaxLength(255).IsRequired();
        builder.Property(p => p.EmailAddress).HasMaxLength(255).IsRequired();
        builder.Property(p => p.PhoneNumber).HasMaxLength(20).IsRequired();

        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<int>(); // Convert enum to int for storage

        // Configure relationship
        builder.HasOne(p => p.Account)
               .WithOne()
               .HasForeignKey<PhotoBoothBranches>(p => p.Account.Id)
               .IsRequired(false);
    }
}
