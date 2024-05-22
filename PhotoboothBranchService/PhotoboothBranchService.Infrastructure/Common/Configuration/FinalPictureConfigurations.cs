using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
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

            builder.Property(fp => fp.LastModified)
                .IsRequired();

            builder.Property(fp => fp.PrintQuantity)
                .IsRequired();

            builder.Property(fp => fp.PictureCost)
                .IsRequired();

            // Privacy enum mapping
            builder.Property(fp => fp.PicturePrivacy)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PhotoPrivacy)Enum.Parse(typeof(PhotoPrivacy), v));

            // Relationship with Layout
            builder.HasOne(fp => fp.Layout)
                .WithMany(l => l.FinalPictures)
                .HasForeignKey(fp => fp.LayoutID)
                .IsRequired();

            // Relationship with Order (one-to-one)
            builder.HasOne(fp => fp.Order)
                .WithOne(o => o.FinalPicture)
                .HasForeignKey<Order>(o => o.PictureID)
                .IsRequired();

            // Relationship with PrintPricing
            builder.HasOne(fp => fp.PrintPricing)
                .WithMany(pp => pp.FinalPictures)
                .HasForeignKey(fp => fp.PrintPricingID)
                .IsRequired();

            // Relationship with EffectsPackLog (one-to-one)
            builder.HasOne(fp => fp.EffectsPackLog)
                .WithOne(ep => ep.FinalPicture)
                .HasForeignKey<EffectsPackLog>(ep => ep.PictureID)
                .IsRequired();

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreateDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
