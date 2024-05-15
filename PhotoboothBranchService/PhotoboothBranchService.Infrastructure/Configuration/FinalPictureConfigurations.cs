using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration
{
    public class FinalPictureConfigurations : IEntityTypeConfiguration<FinalPicture>
    {
        public void Configure(EntityTypeBuilder<FinalPicture> builder)
        {
            // Table name
            builder.ToTable("FinalPictures");

            // Primary key
            builder.HasKey(fp => fp.PictureID);
            builder.Property(fp => fp.PictureID).HasColumnName("Picture ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(fp => fp.PictureURl)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(fp => fp.CreateDate)
                .IsRequired();

            // Privacy enum mapping
            builder.Property(fp => fp.Privacy)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PhotoPrivacy)Enum.Parse(typeof(PhotoPrivacy), v));

            // Relationship with Order
            builder.HasOne(fp => fp.Order)
                .WithMany(o => o.FinalPictures)
                .HasForeignKey(fp => fp.OrderID)
                .IsRequired();

            // Relationship with PrintPricing
            builder.HasOne(fp => fp.PrintPricing)
                .WithMany(pp => pp.FinalPictures)
                .HasForeignKey(fp => fp.PrintPricingID)
                .IsRequired();

            // Relationship with EffectsPack
            builder.HasOne(fp => fp.EffectsPack)
                .WithOne(ep => ep.FinalPicture)
                .HasForeignKey<FinalPicture>(fp => fp.PackID)
                .IsRequired();
        }
    }
}
