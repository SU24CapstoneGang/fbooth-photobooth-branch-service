﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class SessionOrderConfigurations : IEntityTypeConfiguration<SessionOrder>
    {
        public void Configure(EntityTypeBuilder<SessionOrder> builder)
        {
            // Table name
            builder.ToTable("SessionOrders");

            // Primary key
            builder.HasKey(s => s.SessionOrderID);
            builder.Property(s => s.SessionOrderID).HasColumnName("SessionOrderID")
                .ValueGeneratedOnAdd();


            builder.Property(s => s.TotalPrice)
                .IsRequired().HasColumnType("decimal(18, 2)"); ; // Tổng giá

            builder.Property(s => s.EndTime).IsRequired(false);
            builder.Property(a => a.StartTime)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("GETDATE()");
            builder.Property(u => u.ValidateCode).IsRequired();
            // enum mapping
            builder.Property(pb => pb.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (SessionOrderStatus)Enum.Parse(typeof(SessionOrderStatus), v));

            // Mối quan hệ một-nhieu giữa Session và Account
            builder.HasOne(s => s.Account)
                .WithMany(a => a.SessionOrder)
                .HasForeignKey(s => s.AccountID)
                .IsRequired();

            builder.HasMany(s => s.Payments)
                .WithOne(p => p.SessionOrder)
                .HasForeignKey(p => p.SessionOrderID)
                .IsRequired();
            builder.HasMany(s => s.ServiceItems)
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
