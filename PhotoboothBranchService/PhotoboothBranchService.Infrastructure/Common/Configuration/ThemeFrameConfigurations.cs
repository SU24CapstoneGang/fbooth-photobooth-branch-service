﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ThemeFrameConfigurations : IEntityTypeConfiguration<ThemeFrame>
    {
        public void Configure(EntityTypeBuilder<ThemeFrame> builder)
        {
            builder.ToTable("ThemeFrames");

            builder.HasKey(tf => tf.ThemeFrameID);
            builder.Property(tf => tf.ThemeFrameID).HasColumnName("ThemeFrame ID")
           .ValueGeneratedOnAdd();

            builder.Property(tf => tf.ThemeFrameName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(tf => tf.ThemeFrameDescription)
                .HasMaxLength(500);

            builder.HasMany(tf => tf.Frames)
                .WithOne(f => f.ThemeFrame)
                .HasForeignKey(f => f.ThemeFrameID);
        }
    }
}
