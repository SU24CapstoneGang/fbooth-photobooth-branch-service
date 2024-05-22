using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ThemeStickerConfigurations : IEntityTypeConfiguration<ThemeSticker>
    {
        public void Configure(EntityTypeBuilder<ThemeSticker> builder)
        {
            builder.ToTable("ThemeStickers");

            builder.HasKey(ts => ts.ThemeStickerID);
            builder.Property(ts => ts.ThemeStickerID).HasColumnName("ThemeSticker ID")
           .ValueGeneratedOnAdd();

            builder.Property(ts => ts.ThemeStickerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ts => ts.ThemeStickerDescription)
                .HasMaxLength(500);

            builder.HasMany(ts => ts.Stickers)
                .WithOne(s => s.ThemeSticker)
                .HasForeignKey(s => s.ThemeStickerID);

            // Status enum mapping
            builder.Property(ts => ts.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));
        }
    }
}
