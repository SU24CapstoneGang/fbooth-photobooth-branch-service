using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PrintPricingConfigurations : IEntityTypeConfiguration<PrintPricing>
    {
        public void Configure(EntityTypeBuilder<PrintPricing> builder)
        {
            // Table name
            builder.ToTable("PrintPricings");

            // Primary key
            builder.HasKey(pp => pp.PrintPricingID);
            builder.Property(pp => pp.PrintPricingID).HasColumnName("PrintPricing ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(pp => pp.UnitPrice)
                .IsRequired();

            builder.Property(pp => pp.MinQuantity)
            .IsRequired(); // Số lượng tối thiểu

            builder.Property(pp => pp.CreatedDate)
                .IsRequired();

            builder.Property(pp => pp.LastModified);

            // Relationship with FinalPicture
            builder.HasMany(pp => pp.Sessions)
            .WithOne(s => s.PrintPricing)
            .HasForeignKey(s => s.PrintPricingID)
            .IsRequired();

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
