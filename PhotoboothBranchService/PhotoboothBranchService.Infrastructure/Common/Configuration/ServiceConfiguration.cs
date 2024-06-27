using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");
            // Primary key
            builder.HasKey(r => r.ServiceID);
            builder.Property(r => r.ServiceID).HasColumnName("ServiceID")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.ServiceName).IsRequired();
            builder.Property(s => s.ServiceDescription).IsRequired(false);
            builder.Property(a => a.Price).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(a => a.Measure).IsRequired();
            builder.Property(a => a.Unit).IsRequired(); ;


            builder.HasOne(s => s.ServiceType)
                .WithMany(t => t.Services)
                .HasForeignKey(s => s.ServiceTypeID)
                .IsRequired();
            builder.HasMany(s => s.ServiceItems)
                .WithOne(i => i.Service)
                .HasForeignKey(i => i.ServiceID)
                .IsRequired();
            builder.HasData(
                new Service
                {
                    ServiceTypeID = new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"),
                    ServiceDescription = "Take Photo in 30 minutes",
                    ServiceName = "Take Photo in 30 minutes",
                    Measure = 30,
                    Unit = "minutes",
                    Price = 90000,
                    ServiceID = new Guid("c51a05bb-28af-4315-b888-606376ba061b")
                },
                new Service
                {
                    ServiceID = new Guid("30c2a7fb-9164-4164-9f1b-7c334744e559"),
                    ServiceTypeID = new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"),
                    ServiceDescription = "Take Photo in 15 minutes",
                    ServiceName = "Take Photo in 15 minutes",
                    Measure = 15,
                    Unit = "minutes",
                    Price = 50000,
                },
                new Service
                {
                    ServiceID = new Guid("9b0dc075-557c-4b29-b3ba-c8b5841c4c68"),
                    ServiceTypeID = new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"),
                    ServiceDescription = "Take Photo in 60 minutes",
                    ServiceName = "Take Photo in 60 minutes",
                    Measure = 60,
                    Unit = "minutes",
                    Price = 180000,
                },
                new Service
                {
                    ServiceID = new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"),
                    ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                    ServiceDescription = "Make up with Korean stype for 1 people",
                    ServiceName = "Make up with Korean stype for 1 people",
                    Measure = 1,
                    Unit = "people",
                    Price = 100000,
                },
                new Service
                {
                    ServiceID = new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"),
                    ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                    ServiceDescription = "Make up with Korean stype for 2 people",
                    ServiceName = "Combo make up with Korean stype for 2 people",
                    Measure = 2,
                    Unit = "people",
                    Price = 190000,
                },
                new Service
                {
                    ServiceID = new Guid("5d568e18-8883-409b-bc48-6456aeefb4f9"),
                    ServiceTypeID = new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"),
                    ServiceDescription = "Hire this booth for 120 minutes",
                    ServiceName = "Hire this booth for 120 minutes",
                    Measure = 120,
                    Unit = "minutes",
                    Price = 190000,
                }
            );

        }
    }
}
