using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
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

            builder.Property(d => d.LastModified);

            // Discount status enum mapping
            builder.Property(d => d.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (DiscountStatus)Enum.Parse(typeof(DiscountStatus), v));

            // Relationship with Order
            builder.HasMany(d => d.Sessions)
                .WithOne(s => s.Discount)
                .HasForeignKey(s => s.DiscountID)
                .IsRequired(false); // Một ưu đãi có thể không được sử dụng trong bất kỳ Sessions nào

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreateDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
