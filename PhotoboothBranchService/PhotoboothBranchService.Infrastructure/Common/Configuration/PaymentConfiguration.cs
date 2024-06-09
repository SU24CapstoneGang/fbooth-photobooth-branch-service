using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");
            builder.HasKey(s => s.PaymentID);
            builder.Property(s => s.PaymentID).IsRequired().HasColumnName("PaymentID");
            builder.Property(s => s.TransactionID).IsRequired();
            builder.Property(s => s.PaymentDateTime).IsRequired();
            builder.Property(s => s.Description).IsRequired(false);
             // Status enum mapping
            builder.Property(ep => ep.PaymentStatus)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));
            builder.Property(s => s.Signature).IsRequired();
            builder.HasOne(s => s.PaymentMethod)
                .WithMany(a => a.Payments)
                .HasForeignKey(s => s.PaymentMethodID)
                .IsRequired();
            builder.HasOne(a => a.SessionOrder)
                .WithMany(b => b.Payments)
                .HasForeignKey(a => a.SessionOrderID)
                .IsRequired();
        }
    }
}