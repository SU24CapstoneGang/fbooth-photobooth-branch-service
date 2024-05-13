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
    public class EffectPackConfigurations : IEntityTypeConfiguration<EffectsPack>
    {
        public void Configure(EntityTypeBuilder<EffectsPack> builder)
        {
            builder.ToTable("EffectsPacks");
            // Primary key
            builder.HasKey(ep => ep.PackID);

            // Other properties
            builder.Property(ep => ep.CreateDate)
                .IsRequired();

            builder.Property(ep => ep.PackagePrice)
                .IsRequired();


            // Relationship with FinalPicture
            builder.HasOne(ep => ep.FinalPicture)
                .WithOne(fp => fp.EffectsPack)
                .HasForeignKey<EffectsPack>(ep => ep.PackID)
                .IsRequired();

            // Relationship with Layout
            builder.HasOne(ep => ep.Layout)
                .WithMany(l => l.EffectsPacks)
                .HasForeignKey(ep => ep.LayoutID)
                .IsRequired();

            // Relationship with Sticker
            builder.HasOne(ep => ep.Sticker)
                .WithMany(s => s.EffectsPacks)
                .HasForeignKey(ep => ep.StickerID)
                .IsRequired();

            // Relationship with Frame
            builder.HasOne(ep => ep.Frame)
                .WithMany(f => f.EffectsPacks)
                .HasForeignKey(ep => ep.FrameID)
                .IsRequired();

            // Relationship with Filter
            builder.HasOne(ep => ep.Filter)
                .WithMany(f => f.EffectsPacks)
                .HasForeignKey(ep => ep.FilterID)
                .IsRequired();

            // Status enum mapping
            builder.Property(ep => ep.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));
        }
    }
}
