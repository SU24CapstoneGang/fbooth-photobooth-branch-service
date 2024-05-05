using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration;

public class DeviceMapConfiguration : IEntityTypeConfiguration<DeviceMap>
{
    public void Configure(EntityTypeBuilder<DeviceMap> builder)
    {
        builder.ToTable("DeviceMaps");

        builder.HasKey(d => d.Id);

        // Configure relationships
        builder.HasOne(d => d.Camera)
               .WithOne(c => c.DeviceMap)
               .HasForeignKey<DeviceMap>(d => d.Camera.Id)
               .IsRequired(false);

        builder.HasOne(d => d.Printer)
               .WithOne(p => p.DeviceMap)
               .HasForeignKey<DeviceMap>(d => d.Printer.Id)
               .IsRequired(false);
    }
}