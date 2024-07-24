using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
    {

        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceTypes");
            builder.HasKey(s => s.ServiceTypeID);

            builder.Property(s => s.ServiceTypeID)
                .ValueGeneratedOnAdd();
            builder.Property(s => s.ServiceTypeName);
            builder.Property(a => a.Unit).IsRequired();
            builder.Property(s => s.Status).IsRequired();

            builder.HasMany(a => a.Services)
                .WithOne(b => b.ServiceType)
                .HasForeignKey(b => b.ServiceTypeID)
                .IsRequired();

            builder.HasData(
                    new ServiceType
                    {
                        ServiceTypeName = "Make up",
                        ServiceTypeID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
                        Unit = "Set",
                        Status = StatusUse.Available
                    },
                    new ServiceType
                    {
                        ServiceTypeID = new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"),
                        ServiceTypeName = "Send email",
                        Unit = "Times",
                        Status = StatusUse.Available
                    },
                    new ServiceType
                    {
                        ServiceTypeID = new Guid("13bd9e6d-3092-496b-8025-530f5f9c43de"),
                        ServiceTypeName = "Hire booth",
                        Unit = "Minutes",
                        Status = StatusUse.Available
                    },
                    new ServiceType
                    {
                        ServiceTypeID = new Guid("70a5a1fd-9c0b-4109-9638-5b6e63e71eca"),
                        ServiceTypeName = "Print photo",
                        Unit = "Times",
                        Status = StatusUse.Available
                    });
        }
    }
}
