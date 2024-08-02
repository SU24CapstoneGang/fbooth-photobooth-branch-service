using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(s => s.TransactionID);
            builder.Property(s => s.TransactionID).IsRequired();

            builder.Property(s => s.GatewayTransactionID).IsRequired(false);
            builder.Property(s => s.TransactionDateTime).IsRequired();
            builder.Property(s => s.Description).IsRequired(false);
            builder.Property(s => s.Amount).IsRequired();

            // Status enum mapping
            builder.Property(ep => ep.TransactionStatus)
                .IsRequired();
            builder.HasOne(s => s.PaymentMethod)
                .WithMany(a => a.Transactions)
                .HasForeignKey(s => s.PaymentMethodID)
                .IsRequired();
            builder.HasOne(a => a.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(a => a.BookingID)
                .IsRequired();
            builder.HasMany(p => p.Refunds)
                .WithOne(i => i.Transaction)
                .HasForeignKey(u => u.TransactionID)
                .IsRequired();
        }
    }
}