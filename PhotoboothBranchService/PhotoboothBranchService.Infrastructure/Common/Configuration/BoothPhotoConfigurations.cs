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
    public class BoothPhotoConfigurations : IEntityTypeConfiguration<BoothPhoto>
    {
        public void Configure(EntityTypeBuilder<BoothPhoto> builder)
        {
            // Table name
            builder.ToTable("BoothPhotos");

            // Primary key
            builder.HasKey(bp => bp.BoothPhotoId);

            // Properties
            builder.Property(bp => bp.BoothPhotoId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(bp => bp.BoothPhotoUrl)
                .IsRequired(); // Adjust the length as per your requirements

            builder.Property(bp => bp.CouldID)
                .IsRequired(); // Adjust the length as per your requirements

            // Relationships
            builder.HasOne(bp => bp.Booth)
                .WithMany(b => b.BoothPhotos)
                .HasForeignKey(bp => bp.BoothID)
                .OnDelete(DeleteBehavior.Cascade); // Set the delete behavior as required
        }
    }
}
