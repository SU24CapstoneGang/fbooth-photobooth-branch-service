using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
