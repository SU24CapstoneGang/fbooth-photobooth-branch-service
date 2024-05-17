using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.Property(pp => pp.CreatedDate)
                .IsRequired();

            builder.Property(pp => pp.LastModified)
                .IsRequired();

            // Relationship with FinalPicture
            builder.HasMany(pp => pp.FinalPictures)
                .WithOne(fp => fp.PrintPricing)
                .HasForeignKey(fp => fp.PrintPricingID)
                .IsRequired();
        }
    }
}
