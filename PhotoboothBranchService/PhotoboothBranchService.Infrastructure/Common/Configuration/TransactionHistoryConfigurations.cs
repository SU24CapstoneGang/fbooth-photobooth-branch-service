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
            builder.Property(th => th.FinalPictureNumber)
                .IsRequired();

            builder.Property(th => th.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(th => th.CreatedDate)
                .IsRequired();

            // Relationship with Customer
            builder.HasOne(th => th.Account)
                .WithMany(c => c.TransactionHistories)
                .HasForeignKey(th => th.AccountID)
                .IsRequired();
        }
    }
}
