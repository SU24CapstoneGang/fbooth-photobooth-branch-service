using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class FullPaymentPolicyConfigurations : IEntityTypeConfiguration<FullPaymentPolicy>
    {
        public void Configure(EntityTypeBuilder<FullPaymentPolicy> builder)
        {
            builder.HasKey(p => p.FullPaymentPolicyID);

            builder.Property(p => p.PolicyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.PolicyDescription)
                .HasMaxLength(500);

            builder.Property(p => p.RefundDaysBefore)
                .IsRequired();
            builder.Property(p => p.RefundPercent).IsRequired();
            builder.Property(p => p.NoCheckInTimeLimit)
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.Property(p => p.StartDate)
                .IsRequired(false);

            builder.Property(p => p.EndDate)
                .IsRequired(false);

            builder.Property(p => p.CreatedDate)
                .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(p => p.IsPermanentPolicy)
                .IsRequired();

            builder.Property(p => p.IsDefaultPolicy)
                .IsRequired();

            // Configuring relationships (if any)
            builder.HasMany(p => p.Bookings)
                .WithOne(b => b.FullPaymentPolicy)
                .HasForeignKey(b => b.FullPaymentPolicyID);
        }
    }
}
