using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoboothBranchService.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ConstantConfiguration : IEntityTypeConfiguration<Constant>
    {
        public void Configure(EntityTypeBuilder<Constant> builder)
        {
            builder.ToTable("Constants");

            builder.HasKey(c => c.ConstantKey);

            builder.Property(c => c.ConstantValue)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(c => c.DisplayName) 
               .HasMaxLength(255);
            builder.Property(c => c.ConstantType) 
              .IsRequired();
            builder.Property(c => c.Description)
                   .HasColumnType("TEXT");
            builder.Property(c => c.CanUpdateValue).IsRequired();
            builder.Property(c => c.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasData(
                new Constant
                {
                    ConstantKey = "DepositPercent",
                    DisplayName = "Deposit percent",
                    ConstantValue = "20",
                    ConstantType = Domain.Enum.ConstantType.Int,
                    Description = "Percent value of total price when customer choose deposit the session order.",
                    CanUpdateValue = true
                },
                new Constant
                {
                    ConstantKey = "RefundPercent",
                    DisplayName = "Refund percent",
                    ConstantValue = "50",
                    ConstantType = Domain.Enum.ConstantType.Int,
                    Description = "Percent value of total price when customer cancel the session order.",
                    CanUpdateValue = true
                },
                new Constant
                {
                    ConstantKey = "BookingDeadline",
                    DisplayName = "Booking deadline",
                    ConstantValue = "45",
                    ConstantType = Domain.Enum.ConstantType.Int,
                    Description = "The start time for the session order must be at least \"value\" minutes after now. The value is measured in minutes.",
                    CanUpdateValue= true
                },
                new Constant
                {
                    ConstantKey = "CancelDeadlineRefund",
                    DisplayName = "Cancel deadline to refund",
                    ConstantValue = "30",
                    ConstantType = Domain.Enum.ConstantType.Int,
                    Description = "The time to cancel the session order must before \"value\" minutes start time. The value is measured in minutes. If not meet condition, the cancel will not refund transactions.",
                    CanUpdateValue = true,
                },
                new Constant 
                {
                    ConstantKey = "BoothReservationHold",
                    DisplayName = "Booth reservation hold time",
                    ConstantValue = "15",
                    ConstantType = Domain.Enum.ConstantType.Int,
                    Description = "Time to hold the booth if customer not checkin and pay the rest of bill. Then the order will change to Cancel status.",
                    CanUpdateValue = true,
                },
                new Constant
                {
                    ConstantKey = "OpenTime",
                    DisplayName = "Open time",
                    ConstantValue = "8:00",
                    ConstantType = Domain.Enum.ConstantType.Time,
                    Description = "Open time in a day of system, using 24-hour format. With form hh:mm.",
                    CanUpdateValue = false
                },
                new Constant
                {
                    ConstantKey = "CloseTime",
                    DisplayName = "Close time",
                    ConstantValue = "23:00",
                    ConstantType = Domain.Enum.ConstantType.Time,
                    Description = "Open time in a day of system, using 24-hour format. With form hh:mm.",
                    CanUpdateValue = false
                });
        }
    }
}
