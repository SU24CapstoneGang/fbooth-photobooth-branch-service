﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class FrameConfigurations : IEntityTypeConfiguration<Frame>
    {
        public void Configure(EntityTypeBuilder<Frame> builder)
        {
            // Table name
            builder.ToTable("Frames");

            // Primary key
            builder.HasKey(f => f.FrameID);
            builder.Property(f => f.FrameID).HasColumnName("FrameID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(f => f.FrameName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.FrameURL)
                .IsRequired();

            builder.Property(f => f.CreatedDate)
                .IsRequired();

            builder.Property(f => f.Lenght)
                .IsRequired();

            builder.Property(f => f.Width)
                .IsRequired();

            builder.Property(f => f.LastModified);

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // Status enum mapping
            builder.Property(ep => ep.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));

            builder.HasMany(p => p.Photos)
                .WithOne(f => f.Frame)
                .HasForeignKey(f => f.FrameID)
                .IsRequired();;
        }
    }
}
