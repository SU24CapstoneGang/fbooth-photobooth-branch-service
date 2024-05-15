using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration
{
    public class FrameConfigurations : IEntityTypeConfiguration<Frame>
    {
        public void Configure(EntityTypeBuilder<Frame> builder)
        {
            // Table name
            builder.ToTable("Frames");

            // Primary key
            builder.HasKey(f => f.FrameID);
            builder.Property(f => f.FrameID).HasColumnName("Frame ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(f => f.FrameName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.FrameURL)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.CreatedDate)
                .IsRequired();

            builder.Property(f => f.LastModified)
                .IsRequired();

            // Relationship with EffectsPack
            builder.HasMany(f => f.EffectsPacks)
                .WithOne(ep => ep.Frame)
                .HasForeignKey(ep => ep.FrameID)
                .IsRequired();
        }
    }
}
