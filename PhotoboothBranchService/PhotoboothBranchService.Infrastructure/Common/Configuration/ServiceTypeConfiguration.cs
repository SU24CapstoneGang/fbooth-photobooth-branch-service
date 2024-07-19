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
                        ServiceTypeName = "Make up",
                        ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6")
                    },
                    new ServiceType
                    {
                        ServiceTypeID = new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"),
                        ServiceTypeName = "Relate to Take Photo"
                    });
        }
    }
}
