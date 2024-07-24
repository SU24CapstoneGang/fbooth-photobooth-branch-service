using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
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
            builder.Property(ep => ep.PaymentStatus)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));
            builder.HasOne(s => s.PaymentMethod)
                .WithMany(a => a.Payments)
                .HasForeignKey(s => s.PaymentMethodID)
                .IsRequired();
            builder.HasOne(a => a.SessionOrder)
                .WithMany(b => b.Payments)
                .HasForeignKey(a => a.SessionOrderID)
                .IsRequired();
            builder.HasMany(p => p.Refunds)
                .WithOne(i => i.Payment)
                .HasForeignKey(u => u.PaymentID)
                .IsRequired();
        }
    }
}