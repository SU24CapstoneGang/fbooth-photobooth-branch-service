using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PhotoConfigurations : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos");

            builder.HasKey(p => p.PhotoID);
            builder.Property(p => p.PhotoID).HasColumnName("PhotoID").ValueGeneratedOnAdd();

            builder.Property(p => p.PhotoURL).IsRequired();
            builder.Property(p => p.CouldID).IsRequired();
            // ManufactureStatus enum mapping
            builder.Property(pb => pb.Version)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PhotoVersion)Enum.Parse(typeof(PhotoVersion), v));

            //auto add CreateDate and ignore change after update
            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(a => a.PhotoStickers)
                .WithOne(b => b.Photo)
                .HasForeignKey(b => b.PhotoID);
        }
    }
}
