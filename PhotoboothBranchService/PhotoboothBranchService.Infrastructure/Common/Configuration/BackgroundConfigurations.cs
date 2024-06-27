using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BackgroundConfigurations : IEntityTypeConfiguration<Background>
    {
        public void Configure(EntityTypeBuilder<Background> builder)
        {
            // Table name
            builder.ToTable("Backgrounds");

            // Primary key
            builder.HasKey(f => f.BackgroundID);
            builder.Property(f => f.BackgroundID).HasColumnName("BackgroundID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(f => f.BackgroundCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.BackgroundURL).IsRequired();
            builder.Property(f => f.CouldID).IsRequired();
            builder.Property(f => f.CreatedDate).IsRequired();
            builder.Property(f => f.Lenght).IsRequired();
            builder.Property(f => f.Width).IsRequired();
            builder.Property(f => f.LastModified).IsRequired(false);

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreatedDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("GETDATE()")
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // Status enum mapping
            builder.Property(ep => ep.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));

            builder.HasMany(p => p.Photos)
                .WithOne(f => f.Background)
                .HasForeignKey(f => f.BackgroundID)
                .IsRequired(false);
        }
    }
}
