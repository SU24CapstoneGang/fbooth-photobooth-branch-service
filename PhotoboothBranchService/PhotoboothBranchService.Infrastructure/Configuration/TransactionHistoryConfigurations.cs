using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Configuration
{
    public class TransactionHistoryConfigurations : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            // Table name
            builder.ToTable("TransactionHistories");

            // Primary key
            builder.HasKey(th => th.TransactionID);

            // Other properties
            builder.Property(th => th.FinalPictureNumber)
                .IsRequired();

            builder.Property(th => th.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(th => th.CreatedDate)
                .IsRequired();

            // Relationship with Customer
            builder.HasOne(th => th.Customer)
                .WithMany(c => c.TransactionHistories)
                .HasForeignKey(th => th.CustomerID)
                .IsRequired();
        }
    }
}
