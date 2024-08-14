using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(s => s.PaymentID);
            builder.Property(s => s.PaymentID).IsRequired();

            builder.Property(s => s.TransactionID).IsRequired(false);
            builder.Property(s => s.PaymentDateTime).IsRequired();
            builder.Property(s => s.Description).IsRequired(false);
            builder.Property(s => s.Amount).IsRequired();

            // Status enum mapping
            builder.Property(ep => ep.Status)
                .IsRequired();

            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(s => s.PaymentMethod)
                .WithMany(a => a.Payments)
                .HasForeignKey(s => s.PaymentMethodID)
                .IsRequired();
            builder.HasOne(a => a.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(a => a.BookingID)
                .IsRequired();
            builder.HasMany(p => p.Refunds)
                .WithOne(i => i.Payment)
                .HasForeignKey(u => u.PaymentID)
                .IsRequired();
        }
    }
}