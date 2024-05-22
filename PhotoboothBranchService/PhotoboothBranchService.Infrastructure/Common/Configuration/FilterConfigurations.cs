using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class FilterConfigurations : IEntityTypeConfiguration<Filter>
    {
        public void Configure(EntityTypeBuilder<Filter> builder)
        {
            builder.ToTable("Filters");
            // Primary key
            builder.HasKey(f => f.FilterID);
            builder.Property(f => f.FilterID).HasColumnName("Filter ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(f => f.FilterName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.FilterURL)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.CreatedDate)
                .IsRequired();

            builder.Property(f => f.LastModified);

            // Relationship with EffectsPack
            builder.HasMany(f => f.EffectsPackLogs)
                .WithOne(ep => ep.Filter)
                .HasForeignKey(ep => ep.FilterID)
                .IsRequired();

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
        }
    }
}
