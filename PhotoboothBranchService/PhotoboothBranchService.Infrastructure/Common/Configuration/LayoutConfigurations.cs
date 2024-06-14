using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

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
            builder.Property(l => l.LayoutURL).IsRequired();
            builder.Property(l => l.CouldID).IsRequired();
            builder.Property(l => l.Lenght).IsRequired();
            builder.Property(l => l.Width).IsRequired();
            builder.Property(l => l.PhotoSlot).IsRequired();

            // ManufactureStatus enum mapping
            builder.Property(l => l.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));

            builder.Property(l => l.LastModified);

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.HasMany(t => t.PhotoSessions)
                .WithOne(p => p.Layout)
                .HasForeignKey(p => p.LayoutID)
                //.OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(t => t.PhotoBoxes)
                .WithOne(p => p.Layout)
                .HasForeignKey(v => v.LayoutID)
                .IsRequired();
        }
    }
}
