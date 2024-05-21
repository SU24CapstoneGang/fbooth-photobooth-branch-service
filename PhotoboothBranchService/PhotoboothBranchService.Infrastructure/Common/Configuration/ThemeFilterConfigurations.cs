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
    public class ThemeFilterConfigurations : IEntityTypeConfiguration<ThemeFilter>
    {
        public void Configure(EntityTypeBuilder<ThemeFilter> builder)
        {
            builder.ToTable("ThemeFilters");

            builder.HasKey(tf => tf.ThemeFilterID);
            builder.Property(tf => tf.ThemeFilterID).HasColumnName("ThemeFilter ID")
            .ValueGeneratedOnAdd();

            builder.Property(tf => tf.ThemeFilterName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(tf => tf.ThemeFilterDescription)
                .HasMaxLength(500);

            builder.HasMany(tf => tf.Filters)
                .WithOne(f => f.ThemeFilter)
                .HasForeignKey(f => f.ThemeFilterID);
        }
    }
}
