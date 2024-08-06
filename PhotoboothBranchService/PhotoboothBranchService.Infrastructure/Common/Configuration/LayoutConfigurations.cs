using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System.Reflection.Emit;

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
            builder.Property(l => l.LayoutID).HasColumnName("LayoutID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(l => l.LayoutCode).IsRequired();
            builder.Property(l => l.LayoutURL).IsRequired();
            builder.Property(l => l.CouldID).IsRequired();
            builder.Property(l => l.Height).IsRequired();
            builder.Property(l => l.Width).IsRequired();
            builder.Property(l => l.PhotoSlot).IsRequired();

            builder.Property(l => l.Status)
                .IsRequired();

            builder.Property(l => l.LastModified);

            //auto add CreateDate
            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(t => t.PhotoSessions)
                .WithOne(p => p.Layout)
                .HasForeignKey(p => p.LayoutID)
                .IsRequired(false);

            builder.HasMany(t => t.PhotoBoxes)
                .WithOne(p => p.Layout)
                .HasForeignKey(v => v.LayoutID)
                .IsRequired();

            builder.HasMany(i => i.Backgrounds)
                .WithOne(p => p.Layout)
                .HasForeignKey(pt => pt.LayoutID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        }
    }
}
