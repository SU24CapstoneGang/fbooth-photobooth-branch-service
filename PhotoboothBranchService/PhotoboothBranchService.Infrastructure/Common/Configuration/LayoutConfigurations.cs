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

            builder.Property(l => l.LayoutPrice)
                .IsRequired();

            builder.Property(l => l.CreatedDate)
                .IsRequired();

            builder.Property(l => l.LastModified);


            // Mối quan hệ một-nhiều giữa Layout và EffectsPackLog
            builder.HasMany(l => l.EffectsPackLogs)
                .WithOne(e => e.Layout)
                .HasForeignKey(e => e.LayoutID)
                .IsRequired();

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
