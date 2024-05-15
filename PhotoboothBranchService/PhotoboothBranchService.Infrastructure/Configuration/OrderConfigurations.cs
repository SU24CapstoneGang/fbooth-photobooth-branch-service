using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration
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

            builder.Property(o => o.PhotoQuantity)
                .IsRequired();

            builder.Property(o => o.TotalPrice)
                .IsRequired();

            // Relationship with PaymentMethod
            builder.HasOne(o => o.PaymentMethod)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.PaymentID)
                .IsRequired();

            // Relationship with Discount
            builder.HasOne(o => o.Discount)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DiscountID)
                .IsRequired();

            // Relationship with FinalPicture
            builder.HasMany(o => o.FinalPictures)
                .WithOne(fp => fp.Order)
                .HasForeignKey(fp => fp.OrderID)
                .IsRequired();
        }
    }
}
