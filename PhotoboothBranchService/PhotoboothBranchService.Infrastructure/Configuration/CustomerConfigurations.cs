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
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            // Primary key
            builder.HasKey(c => c.CustomerID);

            // Other properties
            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.PasswordHash)
                .IsRequired();

            builder.Property(c => c.PasswordSalt)
                .IsRequired();

            builder.Property(c => c.DateOfBirth)
                .IsRequired();

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11);

            // Account status enum mapping
            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (AccountStatus)Enum.Parse(typeof(AccountStatus), v));

            // Relationship with Session
            builder.HasMany(c => c.Sessions)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerID)
                .IsRequired();

            // Relationship with TransactionHistory
            builder.HasMany(c => c.TransactionHistories)
                .WithOne(th => th.Customer)
                .HasForeignKey(th => th.CustomerID)
                .IsRequired();
        }
    }
}
