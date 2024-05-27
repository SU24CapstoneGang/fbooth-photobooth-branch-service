using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class MapStickerConfigurations : IEntityTypeConfiguration<MapSticker>
    {
        public void Configure(EntityTypeBuilder<MapSticker> builder)
        {
            // Table name
            builder.ToTable("MapStickers");

            // Primary key
            builder.HasKey(ms => ms.MapStickerID);
            builder.Property(ms => ms.MapStickerID).HasColumnName("MapStickerID")
                .ValueGeneratedOnAdd();

            // Relationships
            builder.HasOne(ms => ms.EffectsPackLog)
                .WithMany(epl => epl.MapStickers) // Assuming EffectsPackLog has a collection of MapStickers
                .HasForeignKey(ms => ms.PackLogID)
                .IsRequired();

            builder.HasOne(ms => ms.Sticker)
                .WithMany(s => s.MapStickers) // Assuming Sticker has a collection of MapStickers
                .HasForeignKey(ms => ms.StickerId)
                .IsRequired(false);
        }
    }
}
