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

            builder.Property(s => s.LastModified)
                .IsRequired();

            // Relationship with EffectsPackLog
            builder.HasOne(s => s.EffectsPackLog)
                .WithMany(ep => ep.Stickers)
                .HasForeignKey(s => s.PackID)
                .IsRequired();
        }
    }
}
