﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Table name
            builder.ToTable("Bookings");

            // Primary key
            builder.HasKey(s => s.BookingID);
            builder.Property(s => s.BookingID).ValueGeneratedOnAdd();

            builder.Property(s => s.TotalPrice).IsRequired().HasColumnType("decimal(18, 0)"); ; // Tổng giá
            builder.Property(s => s.CustomerBusinessID).IsRequired();
            builder.Property(s => s.HireBoothFee).IsRequired().HasColumnType("decimal(18, 0)");
            builder.Property(s => s.PaidAmount).IsRequired().HasColumnType("decimal(18, 0)").HasDefaultValue(0);
            builder.Property(s => s.RefundedAmount).IsRequired().HasColumnType("decimal(18, 0)").HasDefaultValue(0);
            builder.Property(s => s.StartTime).IsRequired(true);
            builder.Property(s => s.EndTime).IsRequired(true);

            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");
            builder.Property(c => c.LastModified)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                .ValueGeneratedOnAddOrUpdate();
            builder.Property(u => u.ValidateCode).IsRequired();
            builder.Property(pb => pb.BookingStatus).IsRequired();

            // Mối quan hệ một-nhieu giữa Session và Account
            builder.HasOne(s => s.Account)
                .WithMany(a => a.Bookings)
                .HasForeignKey(s => s.CustomerID)
                .IsRequired();

            builder.HasMany(s => s.Payments)
                .WithOne(p => p.Booking)
                .HasForeignKey(p => p.BookingID)
                .IsRequired();
            builder.HasMany(s => s.BookingServices)
                .WithOne(a => a.Booking)
                .HasForeignKey(c => c.BookingID)
                .IsRequired(false);
            builder.HasMany(s => s.PhotoSessions)
                .WithOne(ps => ps.Booking)
                .HasForeignKey(ps => ps.BookingID)
                .IsRequired();
            builder.HasMany(s => s.BookingSlots)
                .WithOne(ps => ps.Booking)
                .HasForeignKey(ps => ps.BookingID)
                .IsRequired();
        }
    }
}
