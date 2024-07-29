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


            builder.Property(s => s.PaymentAmount)
                .IsRequired().HasColumnType("decimal(18, 2)"); ; // Tổng giá

            builder.Property(s => s.StartTime).IsRequired(true);
            builder.Property(s => s.EndTime).IsRequired(true);

            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.ValidateCode).IsRequired();
            // enum mapping
            builder.Property(pb => pb.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v));

            // Mối quan hệ một-nhieu giữa Session và Account
            builder.HasOne(s => s.Account)
                .WithMany(a => a.SessionOrder)
                .HasForeignKey(s => s.CustomerID)
                .IsRequired();

            builder.HasMany(s => s.Payments)
                .WithOne(p => p.SessionOrder)
                .HasForeignKey(p => p.SessionOrderID)
                .IsRequired();
            builder.HasMany(s => s.BookingServices)
                .WithOne(a => a.SessionOrder)
                .HasForeignKey(c => c.SessionOrderID)
                .IsRequired(false);
            builder.HasMany(s => s.PhotoSessions)
                .WithOne(ps => ps.SessionOrder)
                .HasForeignKey(ps => ps.SessionOrderID)
                .IsRequired();
        }
    }
}
