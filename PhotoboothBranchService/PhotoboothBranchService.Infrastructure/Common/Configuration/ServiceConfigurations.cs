using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ServiceConfigurations : IEntityTypeConfiguration<Service>
    {

        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(s => s.ServiceID);

            builder.Property(s => s.ServiceID)
                .ValueGeneratedOnAdd();
            builder.Property(s => s.ServiceName);
            builder.Property(s => s.ServiceDescription);
            builder.Property(s => s.ServicePrice).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(s => s.ServiceType).IsRequired();

            builder.Property(a => a.Unit).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();
            builder.HasMany(i => i.BookingServices)
                .WithOne(a => a.Service)
                .HasForeignKey(b => b.ServiceID);
            builder.HasData(
                new Service
                {
                    ServiceID = new Guid("d1f4e0c1-1f62-4c9e-8c4a-123456789abc"),
                    ServiceName = "Makeup Kit Rental",
                    ServiceDescription = "Professional makeup kit rental",
                    Unit = "Set",
                    ServicePrice = 300000m, // Price in VND
                    Status = StatusUse.Available,
                    ServiceType = Domain.ServiceType.Other,
                    CreatedDate = DateTime.Now,
                    LastModified = DateTime.Now,
                },
                new Service
                {
                    ServiceID = new Guid("f2e3d4c5-b2b7-4bcb-93c8-23456789defa"),
                    ServiceName = "Send Photos via Email",
                    ServiceDescription = "Service for sending photos via email",
                    Unit = "Photo",
                    ServicePrice = 50000m, // Price in VND
                    Status = StatusUse.Available,
                    ServiceType = Domain.ServiceType.EmailSending,
                    CreatedDate = DateTime.Now,
                    LastModified = DateTime.Now,
                },
                new Service
                {
                    ServiceID = new Guid("b3c4d5e6-a3d7-4edf-a3b9-34567890bcde"),
                    ServiceName = "Photo Printing",
                    ServiceDescription = "High-quality photo printing service",
                    Unit = "Piece",
                    ServicePrice = 150000m, // Price in VND
                    Status = StatusUse.Available,
                    ServiceType = Domain.ServiceType.Printing,
                    CreatedDate = DateTime.Now,
                    LastModified = DateTime.Now,
                });
        }
    }
}
