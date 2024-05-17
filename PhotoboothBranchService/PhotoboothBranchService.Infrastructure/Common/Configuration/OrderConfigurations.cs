using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Table name
            builder.ToTable("Orders");

            // Primary key
            builder.HasKey(o => o.OrderID);
            builder.Property(o => o.OrderID).HasColumnName("Order ID")
                .ValueGeneratedOnAdd();


            // Other properties
            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.QuantityOfPicture)
                .IsRequired();

            builder.Property(o => o.TotalPrice)
                .IsRequired();

            // Relationship with Session
            builder.HasOne(o => o.Session)
                .WithOne(s => s.Order)
                .HasForeignKey<Order>(o => o.SessionID)
                .IsRequired();

            // Relationship with PaymentMethod
            builder.HasOne(o => o.PaymentMethod)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.PaymentID)
                .IsRequired();

            // Relationship with Account
            builder.HasOne(o => o.Account)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AccountID)
                .IsRequired(false);

            // Relationship with Discount
            builder.HasOne(o => o.Discount)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DiscountID)
                .IsRequired();

            // Relationship with FinalPicture
            builder.HasOne(o => o.FinalPicture)
                .WithOne(fp => fp.Order)
                .HasForeignKey<Order>(o => o.PictureID)
                .IsRequired();
        }
    }
}
