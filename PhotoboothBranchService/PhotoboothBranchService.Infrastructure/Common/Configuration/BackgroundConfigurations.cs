﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System.Reflection.Emit;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BackgroundConfigurations : IEntityTypeConfiguration<Background>
    {
        public void Configure(EntityTypeBuilder<Background> builder)
        {
            // Table name
            builder.ToTable("Backgrounds");

            // Primary key
            builder.HasKey(f => f.BackgroundID);
            builder.Property(f => f.BackgroundID).HasColumnName("BackgroundID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(f => f.BackgroundCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.BackgroundURL).IsRequired();
            builder.Property(f => f.CouldID).IsRequired();
            builder.Property(f => f.CreatedDate).IsRequired();
            builder.Property(f => f.Height).IsRequired();
            builder.Property(f => f.Width).IsRequired();

            //auto add CreateDate
            builder.Property(c => c.CreatedDate)
                  .IsRequired()
                  .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            // Status enum mapping
            builder.Property(ep => ep.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));

            builder.HasMany(p => p.Photos)
                .WithOne(f => f.Background)
                .HasForeignKey(f => f.BackgroundID)
            .IsRequired(false);

        }
    }
}
