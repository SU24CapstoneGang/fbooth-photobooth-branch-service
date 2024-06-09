using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos");

            builder.HasKey(p => p.PhotoID);
            builder.Property(p => p.PhotoID).HasColumnName("PhotoID").ValueGeneratedOnAdd();

            builder.Property(p => p.PhotoURL).IsRequired();

            // ManufactureStatus enum mapping
            builder.Property(pb => pb.Version)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PhotoVersion)Enum.Parse(typeof(PhotoVersion), v));

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreateDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.HasOne(l => l.Layout)
                .WithMany(a => a.Photos)
                .HasForeignKey(l => l.LayoutID)
                .IsRequired();

            builder.HasOne(l => l.Frame)
              .WithMany(a => a.Photos)
              .HasForeignKey(l => l.FrameID)
              .IsRequired();

            builder.HasOne(l => l.Filter)
              .WithMany(a => a.Photos)
              .HasForeignKey(l => l.FilterID)
              .IsRequired();

            builder.HasOne(l => l.PhotoSession)
              .WithMany(a => a.Photos)
              .HasForeignKey(l => l.PhotoSessionID)
              .IsRequired();

            builder.HasMany(a => a.PhotoStickers)
                .WithOne(b => b.Photo)
                .HasForeignKey(b => b.PhotoID);
        }
    }
}
