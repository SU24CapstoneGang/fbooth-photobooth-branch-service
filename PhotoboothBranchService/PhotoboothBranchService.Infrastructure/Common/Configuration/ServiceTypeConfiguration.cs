using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
    {

        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceTypes");
            builder.HasKey(s => s.ServiceTypeID);
            builder.Property(s => s.ServiceTypeID)
                .HasColumnName("ServiceTypeID")
                .ValueGeneratedOnAdd();
            builder.Property(s => s.ServiceTypeName);
            builder.HasMany(a => a.Services)
                .WithOne(b => b.ServiceType)
                .HasForeignKey(b => b.ServiceTypeID)
                .IsRequired();

            builder.HasData(
                new ServiceType
                {
                    ServiceTypeName = "Take photo",
                    ServiceTypeID = new Guid("79497fe3-789f-4555-ac6b-977dfbf0f671")
                },
                new ServiceType
                {
                    ServiceTypeName = "Make up",
                    ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6")
                },
                new ServiceType
                {
                    ServiceTypeID = new Guid("96eae221-6528-4f26-a97b-3ad5e55256ee"),
                    ServiceTypeName = "Hire booth"
                }
            );
        }
    }
}
