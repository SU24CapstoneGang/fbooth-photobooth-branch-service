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
    public class DiscountConfigurations : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            // Primary key
            builder.HasKey(d => d.DiscountID);
            builder.Property(d => d.DiscountID).HasColumnName("Discount ID")
                .ValueGeneratedOnAdd();
            // Properties
            builder.Property(d => d.DiscountCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.RemaniningUsage)
                .IsRequired();

            builder.Property(d => d.DiscountRate)
                .IsRequired();

            builder.Property(d => d.CreateDate)
                .IsRequired();

            builder.Property(d => d.LastModified)
                .IsRequired();

            // Discount status enum mapping
            builder.Property(d => d.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (DiscountStatus)Enum.Parse(typeof(DiscountStatus), v));

            // Relationship with Order
            builder.HasMany(d => d.Orders)
                .WithOne(o => o.Discount)
                .HasForeignKey(o => o.DiscountID)
                .IsRequired(false); // Một ưu đãi có thể không được sử dụng trong bất kỳ đơn hàng nào
        }
    }
}
