using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class StickerConfigurations : IEntityTypeConfiguration<Sticker>
    {
        public void Configure(EntityTypeBuilder<Sticker> builder)
        {
            // Table name
            builder.ToTable("Stickers");

            // Primary key
            builder.HasKey(s => s.StickerId);
            builder.Property(s => s.StickerId).HasColumnName("Sticker ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(s => s.StickerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.StrickerURL)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.CreatedDate)
                .IsRequired();

            builder.Property(s => s.LastModified);

            // Relationship with MapSticker
            builder.HasMany(s => s.MapStickers)
               .WithOne(ms => ms.Sticker)
               .HasForeignKey(ms => ms.StickerId)
               .IsRequired();

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
