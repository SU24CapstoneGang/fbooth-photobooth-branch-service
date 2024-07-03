using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices");
            // Primary key
            builder.HasKey(u => u.DeviceID);
            builder.Property(u => u.DeviceID).HasColumnName("DeviceID").ValueGeneratedOnAdd();
            builder.Property(b => b.DeviceName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.CreatedDate)
             .ValueGeneratedOnAdd()
             .HasDefaultValueSql("GETDATE()");
        }
    }
}
