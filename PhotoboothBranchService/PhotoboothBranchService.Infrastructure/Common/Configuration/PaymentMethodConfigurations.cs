using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PaymentMethodConfigurations : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            // Table name
            builder.ToTable("PaymentMethods");

            // Primary key
            builder.HasKey(pm => pm.PaymentID);
            builder.Property(pm => pm.PaymentID).HasColumnName("Payment ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(pm => pm.PaymentName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pm => pm.CreateDate)
                .IsRequired();

            // Payment status enum mapping
            builder.Property(pm => pm.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));

            // Relationship with Order
            builder.HasMany(pm => pm.Orders)
                .WithOne(o => o.PaymentMethod)
                .HasForeignKey(o => o.PaymentID)
                .IsRequired();
        }
    }
}
