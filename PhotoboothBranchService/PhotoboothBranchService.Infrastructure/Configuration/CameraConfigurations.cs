using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration;

public class CameraConfigurations : IEntityTypeConfiguration<Camera>
{
    public void Configure(EntityTypeBuilder<Camera> builder)
    {
        builder.ToTable("Cameras");

        // Primary key
        builder.HasKey(c => c.CameraID);
        builder.Property(c => c.CameraID).HasColumnName("Camera ID")
            .ValueGeneratedOnAdd();

        // Properties
        builder.Property(c => c.ModelName)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(c => c.LensType)
            .HasMaxLength(50);

        builder.Property(c => c.Lens)
            .HasMaxLength(100);

        builder.Property(c => c.Price)
            .IsRequired();

        // Status enum mapping
        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (ManufactureStatus)Enum.Parse(typeof(ManufactureStatus), v));

        // Relationship with PhotoBoothBranch
        builder.HasOne(c => c.PhotoBoothBranch)
            .WithOne(pb => pb.Camera)
            .HasForeignKey<Camera>(c => c.PhotoBoothBranchId)
            .IsRequired();
    }
}
