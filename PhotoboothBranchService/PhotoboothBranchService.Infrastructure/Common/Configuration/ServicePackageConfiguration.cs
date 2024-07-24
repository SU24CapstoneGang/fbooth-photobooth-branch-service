using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ServicePackageConfiguration : IEntityTypeConfiguration<ServicePackage>
    {
        public void Configure(EntityTypeBuilder<ServicePackage> builder)
        {
            builder.ToTable("ServicePackages");
            // Primary key
            builder.HasKey(r => r.ServiceID);
            builder.Property(r => r.ServiceID)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.ServiceName).IsRequired();
            builder.Property(s => s.ServiceDescription).IsRequired(false);
            builder.Property(a => a.Price).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(a => a.Measure).IsRequired();

            builder.HasOne(s => s.ServiceType)
                .WithMany(t => t.Services)
                .HasForeignKey(s => s.ServiceTypeID)
                .IsRequired();
            builder.HasMany(s => s.ServiceItems)
                .WithOne(i => i.Service)
                .HasForeignKey(i => i.ServiceID)
                .IsRequired();
            builder.HasData(
                    new ServicePackage
                    {
                        ServiceID = new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"),
                        ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                        ServiceDescription = "Make up with Korean stype for 1 people",
                        ServiceName = "Make up with Korean stype for 1 people",
                        Measure = 1,
                        Price = 100000,
                    },
                    new ServicePackage
                    {
                        ServiceID = new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"),
                        ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                        ServiceDescription = "Make up with Korean stype for 2 people",
                        ServiceName = "Combo make up with Korean stype for 2 people",
                        Measure = 2,
                        Price = 190000,
                    });
        }
    }
}
