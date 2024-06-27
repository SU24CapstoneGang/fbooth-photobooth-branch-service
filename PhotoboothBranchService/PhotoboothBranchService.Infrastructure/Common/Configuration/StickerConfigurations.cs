using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class StickerConfigurations : IEntityTypeConfiguration<Sticker>
    {
        public void Configure(EntityTypeBuilder<Sticker> builder)
        {
            // Table name
            builder.ToTable("Stickers");

            // Primary key
            builder.HasKey(s => s.StickerID);
            builder.Property(s => s.StickerID).HasColumnName("StickerID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(s => s.StickerCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.StrickerURL)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.CouldID)
                .IsRequired();
            builder.Property(s => s.stickerWidth)
               .IsRequired(); 
            builder.Property(s => s.stickerLength)
                .IsRequired();
            builder.Property(s => s.CreatedDate)
                .IsRequired();

            builder.Property(s => s.LastModified);
            // Status enum mapping
            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));

            // Relationship with MapSticker
            builder.HasMany(s => s.PhotoSticker)
               .WithOne(ms => ms.Sticker)
               .HasForeignKey(ms => ms.StickerID)
               .IsRequired();

            //auto add CreateDate and ignore change after update
            builder.Property(s => s.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("GETDATE()")
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // Status enum mapping
            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));
        }
    }
}
