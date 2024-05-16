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
    public class EffectPackConfigurations : IEntityTypeConfiguration<EffectsPackLog>
    {
        public void Configure(EntityTypeBuilder<EffectsPackLog> builder)
        {
            builder.ToTable("EffectsPackLogs");
            // Primary key
            builder.HasKey(ep => ep.PackID);
            builder.Property(ep => ep.PackID).HasColumnName("Pack ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(ep => ep.CreateDate)
                .IsRequired();

            // Relationship with FinalPicture
            builder.HasOne(ep => ep.FinalPicture)
                .WithOne(fp => fp.EffectsPackLog)
                .HasForeignKey<EffectsPackLog>(e => e.PictureID)
                .IsRequired();


            // Relationship with Sticker
            builder.HasMany(ep => ep.Stickers)
                .WithOne(s => s.EffectsPackLog)
                .HasForeignKey(ep => ep.PackID)
                .IsRequired();

            // Relationship with Frame
            builder.HasOne(ep => ep.Frame)
                .WithMany(f => f.EffectsPackLogs)
                .HasForeignKey(ep => ep.FrameID)
                .IsRequired();

            // Relationship with Filter
            builder.HasOne(ep => ep.Filter)
                .WithMany(f => f.EffectsPackLogs)
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
