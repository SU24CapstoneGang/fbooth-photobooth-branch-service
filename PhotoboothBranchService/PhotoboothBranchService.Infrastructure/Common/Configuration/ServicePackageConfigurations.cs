using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ServicePackageConfigurations : IEntityTypeConfiguration<ServicePackage>
    {
        public void Configure(EntityTypeBuilder<ServicePackage> builder)
        {
            builder.ToTable("ServicePackages");
            // Primary key
            builder.HasKey(r => r.ServicePackageID);
            builder.Property(r => r.ServicePackageID)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.PackageName).IsRequired();
            builder.Property(s => s.PackageDescription).IsRequired(false);
            builder.Property(a => a.PackagePrice).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(a => a.Measure).IsRequired();

            builder.HasOne(s => s.Service)
                .WithMany(t => t.ServicePackages)
                .HasForeignKey(s => s.ServiceID)
                .IsRequired();
            builder.HasMany(s => s.BookingServices)
                .WithOne(i => i.Service)
                .HasForeignKey(i => i.ServiceID)
                .IsRequired();

            //builder.HasData(
            //        new ServicePackage
            //        {
            //            ServicePackageID = new Guid("6288e0ab-adec-4363-b80f-95abf3053d56"),
            //            ServiceID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
            //            PackageDescription = "Make up with Korean stype for 1 people",
            //            PackageName = "Make up with Korean stype for 1 people",
            //            Measure = 1,
            //            PackagePrice = 100000,
            //        },
            //        new ServicePackage
            //        {
            //            ServicePackageID = new Guid("13e75fc2-f38b-401e-9cd2-c545b80fd1f0"),
            //            ServiceID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
            //            PackageDescription = "Make up with Korean stype for 2 people",
            //            PackageName = "Combo make up with Korean stype for 2 people",
            //            Measure = 2,
            //            PackagePrice = 190000,
            //        });
        }
    }
}
