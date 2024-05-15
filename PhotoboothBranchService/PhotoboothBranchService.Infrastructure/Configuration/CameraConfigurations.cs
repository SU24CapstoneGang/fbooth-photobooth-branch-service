﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
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
        builder.Property(c => c.ModelName).HasMaxLength(100).IsRequired();
        builder.Property(c => c.SensorType).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Lens).HasMaxLength(255).IsRequired();
        builder.Property(c => c.Price).IsRequired();

        // Relationship with PhotoBoothBranch
        builder.HasOne(c => c.PhotoBoothBranch)
            .WithOne(pb => pb.Camera)
            .HasForeignKey<Camera>(c => c.PhotoBoothBranchId)
            .IsRequired();
    }
}
