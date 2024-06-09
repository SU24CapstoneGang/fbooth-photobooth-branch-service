using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ThemeConfigurations : IEntityTypeConfiguration<Theme>
    {
        public void Configure(EntityTypeBuilder<Theme> builder)
        {
            builder.ToTable("Themes");

            builder.HasKey(tf => tf.ThemeID);
            builder.Property(tf => tf.ThemeID).HasColumnName("ThemeID")
                .ValueGeneratedOnAdd();
            builder.Property(tf => tf.ThemeName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(tf => tf.ThemeFrameDescription)
                .HasMaxLength(500);

            builder.HasMany(tf => tf.Frames)
                .WithOne(f => f.Theme)
                .HasForeignKey(f => f.ThemeID);

        }
    }
}
