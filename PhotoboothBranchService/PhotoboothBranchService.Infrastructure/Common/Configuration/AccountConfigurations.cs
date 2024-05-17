using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration;

public class AccountConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        // Primary key
        builder.HasKey(u => u.AccountID);
        builder.Property(u => u.AccountID).HasColumnName("Account ID")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PhoneNumber).HasMaxLength(11);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(a => a.DateOfBirth).IsRequired();
        builder.Property(a => a.Address).IsRequired().HasMaxLength(100);

        // Relationship with Role
        builder.HasOne(a => a.Role)
                .WithMany(r => r.Accounts)
                .HasForeignKey(a => a.RoleID)
                .IsRequired();

        // Relationship with PhotoBoothBranch
        builder.HasMany(a => a.PhotoBoothBranches)
            .WithOne(pb => pb.Account)
            .HasForeignKey(a => a.AccountID)
            .IsRequired();

        // Relationship with Order
        builder.HasMany(a => a.Orders)
                .WithOne(o => o.Account)
                .HasForeignKey(o => o.AccountID)
                .IsRequired(false);

        // Relationship with TransactionHistory
        builder.HasMany(a => a.TransactionHistories)
            .WithOne(s => s.Account)
            .HasForeignKey(s => s.AccountID)
            .IsRequired();

        // Account status enum mapping
        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (AccountStatus)Enum.Parse(typeof(AccountStatus), v));

        // Indexes
        builder.HasIndex(a => a.Email).IsUnique();
    }
}
