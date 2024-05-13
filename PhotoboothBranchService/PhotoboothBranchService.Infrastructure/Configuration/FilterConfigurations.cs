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
    public class FilterConfigurations : IEntityTypeConfiguration<Filter>
    {
        public void Configure(EntityTypeBuilder<Filter> builder)
        {
            builder.ToTable("Filters");
            // Primary key
            builder.HasKey(f => f.FilterID);

            // Other properties
            builder.Property(f => f.FilterName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.FilterURL)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.CreatedDate)
                .IsRequired();

            builder.Property(f => f.LastModified)
                .IsRequired();

            // Relationship with EffectsPack
            builder.HasMany(f => f.EffectsPacks)
                .WithOne(ep => ep.Filter)
                .HasForeignKey(ep => ep.FilterID)
                .IsRequired();
        }
    }
}
