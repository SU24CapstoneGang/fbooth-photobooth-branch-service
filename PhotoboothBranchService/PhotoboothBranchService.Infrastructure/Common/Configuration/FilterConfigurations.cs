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
            builder.Property(f => f.FilterID).HasColumnName("FilterID")
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
                .WithOne(a => a.Filter)
                .HasForeignKey(a => a.FilterID)
                .IsRequired();
        }
    }
}
