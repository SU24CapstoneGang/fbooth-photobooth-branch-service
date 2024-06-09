using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration;

public class AccountConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        // Primary key
        builder.HasKey(u => u.AccountID);
        builder.Property(u => u.AccountID).HasColumnName("AccountID")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.AccountFBID).IsRequired();
        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PhoneNumber).HasMaxLength(11);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(a => a.DateOfBirth).IsRequired();
        builder.Property(a => a.Address).IsRequired().HasMaxLength(100);

        // Account role  enum mapping
        builder.Property(a => a.Role)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (AccountRole)Enum.Parse(typeof(AccountRole), v));

        // Relationship with Sessions
        builder.HasMany(a => a.SessionOrder)
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
