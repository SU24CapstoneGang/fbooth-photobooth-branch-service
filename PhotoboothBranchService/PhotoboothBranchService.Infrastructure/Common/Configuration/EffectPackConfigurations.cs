﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class EffectPackConfigurations : IEntityTypeConfiguration<EffectsPackLog>
    {
        public void Configure(EntityTypeBuilder<EffectsPackLog> builder)
        {
            builder.ToTable("EffectsPackLogs");
            // Primary key
            builder.HasKey(ep => ep.PacklogID);
            builder.Property(ep => ep.PacklogID).HasColumnName("Pack ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(ep => ep.CreateDate)
                .IsRequired();

            // Relationship with FinalPicture
            builder.HasOne(ep => ep.FinalPicture)
                .WithOne(fp => fp.EffectsPackLog)
                .HasForeignKey<EffectsPackLog>(e => e.PictureID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();


            // Relationship with MapSticker
            builder.HasMany(epl => epl.MapStickers)
                .WithOne(ms => ms.EffectsPackLog)
                .HasForeignKey(ms => ms.PackLogID)
                .IsRequired();

            // Relationship with Frame
            builder.HasOne(ep => ep.Frame)
                .WithMany(f => f.EffectsPackLogs)
                .HasForeignKey(ep => ep.FrameID)
                .IsRequired(false);

            // Relationship with Filter
            builder.HasOne(ep => ep.Filter)
                .WithMany(f => f.EffectsPackLogs)
                .HasForeignKey(ep => ep.FilterID)
                .IsRequired(false);

            // Relationship with Layout
            builder.HasOne(epl => epl.Layout)
            .WithMany(l => l.EffectsPackLogs)
            .HasForeignKey(epl => epl.LayoutID)
            .IsRequired();
        }
    }
}
