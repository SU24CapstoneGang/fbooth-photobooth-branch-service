using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.Property(l => l.LayoutID).HasColumnName("Layout ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(l => l.LayoutURL)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(l => l.LayoutPrice)
                .IsRequired();

            builder.Property(l => l.CreatedDate)
                .IsRequired();

            builder.Property(l => l.LastModified)
                .IsRequired();

            // Relationship with EffectsPack
            builder.HasMany(l => l.FinalPictures)
                .WithOne(ep => ep.Layout)
                .HasForeignKey(ep => ep.LayoutID)
                .IsRequired();
        }
    }
}
