using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BranchPhotoConfigurations : IEntityTypeConfiguration<BranchPhoto>
    {
        public void Configure(EntityTypeBuilder<BranchPhoto> builder)
        {
            // Table name
            builder.ToTable("BranchPhotos");

            // Primary key
            builder.HasKey(bp => bp.BranchPhotoId);

            // Properties
            builder.Property(bp => bp.BranchPhotoId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(bp => bp.BranchPhotoUrl)
                .IsRequired(); // Adjust the length as per your requirements

            builder.Property(bp => bp.CouldID)
                .IsRequired(); // Adjust the length as per your requirements

            // Relationships
            builder.HasOne(bp => bp.Branch)
                .WithMany(b => b.BranchPhotos)
                .HasForeignKey(bp => bp.BranchID)
                .OnDelete(DeleteBehavior.Cascade); // Set the delete behavior as required
        }
    }
}
