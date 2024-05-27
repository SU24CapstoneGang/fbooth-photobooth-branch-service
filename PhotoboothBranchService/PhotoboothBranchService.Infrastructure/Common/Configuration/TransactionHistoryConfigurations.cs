using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class TransactionHistoryConfigurations : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            // Table name
            builder.ToTable("TransactionHistories");

            // Primary key
            builder.HasKey(th => th.TransactionID);
            builder.Property(th => th.TransactionID).HasColumnName("Transaction ID")
                .ValueGeneratedOnAdd();

            
            // Other properties

            builder.Property(th => th.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(th => th.CreatedDate)
                .IsRequired();

            builder.Property(th => th.LastModified).IsRequired(false);

            // Mối quan hệ một-một giữa TransactionHistory và Session
            builder.HasOne(th => th.Session)
                .WithOne(s => s.TransactionHistory)
                .HasForeignKey<TransactionHistory>(th => th.SessionID)
                .IsRequired();

            builder.HasOne(th => th.PaymentMethod)
            .WithMany(pt => pt.TransactionHistories) // Một PaymentMethod có thể được sử dụng trong nhiều TransactionHistory
            .HasForeignKey(th => th.PaymentMethodID)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
