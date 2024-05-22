using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

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
