﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class LayoutConfigurations : IEntityTypeConfiguration<Layout>
    {
        public void Configure(EntityTypeBuilder<Layout> builder)
        {
            // Table name
            builder.ToTable("Layouts");

            // Primary key
            builder.HasKey(l => l.LayoutID);
            builder.Property(l => l.LayoutID).HasColumnName("LayoutID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(l => l.LayoutURL)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(l => l.CreatedDate)
                .IsRequired();

            builder.Property(l => l.LastModified);

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.HasOne(l => l.Frame)
                .WithMany(f => f.Layouts)
                .HasForeignKey(l => l.FrameID)
                .IsRequired();
            builder.HasMany(t => t.Photos)
                .WithOne(p => p.Layout)
                .HasForeignKey(p => p.LayoutID)
                .IsRequired();
        }
    }
}
