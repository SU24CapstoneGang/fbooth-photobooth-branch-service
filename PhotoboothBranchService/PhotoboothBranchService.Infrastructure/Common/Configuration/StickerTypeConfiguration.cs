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
    public class StickerTypeConfiguration : IEntityTypeConfiguration<StickerType>
    {
        public void Configure(EntityTypeBuilder<StickerType> builder)
        {
            builder.ToTable("StickerTypes");
            builder.HasKey(i => i.StickerTypeID);
            builder.Property(s => s.StickerTypeID).ValueGeneratedOnAdd();
            builder.Property(s => s.StickerTypeName).IsRequired();
            builder.Property(s => s.RepresentImageURL).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.CouldID).IsRequired();
            builder.Property(c => c.CreatedDate)
               .IsRequired()
               .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(i => i.Stickers)
                .WithOne(j => j.StickerType)
                .HasForeignKey(m => m.StickerTypeID)
                .IsRequired();
        }
    }
}
