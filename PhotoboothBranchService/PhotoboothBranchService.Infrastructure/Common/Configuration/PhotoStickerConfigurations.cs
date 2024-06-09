using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PhotoStickerConfigurations : IEntityTypeConfiguration<PhotoSticker>
    {
        public void Configure(EntityTypeBuilder<PhotoSticker> builder)
        {
            // Table name
            builder.ToTable("PhotoStickers");

            // Primary key
            builder.HasKey(ms => ms.PhotoStickerID);
            builder.Property(ms => ms.PhotoStickerID).HasColumnName("PhotoStickerID")
                .ValueGeneratedOnAdd();
            builder.Property(ms => ms.Quantity).IsRequired();

            builder.HasOne(ms => ms.Sticker)
                .WithMany(s => s.PhotoSticker) // Assuming Sticker has a collection of MapStickers
                .HasForeignKey(ms => ms.StickerID)
                .IsRequired();
            builder.HasOne(ms => ms.Photo)
                .WithMany(s => s.PhotoStickers)
                .HasForeignKey(ms => ms.StickerID)
                .IsRequired();
        }
    }
}
