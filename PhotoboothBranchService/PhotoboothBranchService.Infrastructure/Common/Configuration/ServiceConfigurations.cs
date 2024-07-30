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


            builder.Property(a => a.Unit).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.HasMany(i => i.BookingServices)
                .WithOne(a => a.Service)
                .HasForeignKey(b => b.ServiceID);
            //builder.HasData(
            //        new Service
            //        {
            //            ServiceName = "Make up",
            //            ServiceID = new Guid("06167451-8b59-4dd2-bb9e-88df025eead6"),
            //            Unit = "Set",
            //            Status = StatusUse.Available
            //        },
            //        new Service
            //        {
            //            ServiceID = new Guid("fc34dccb-10a0-4643-84bf-71ac85ca77bb"),
            //            ServiceName = "Send email",
            //            Unit = "Times",
            //            Status = StatusUse.Available
            //        },
            //        new Service
            //        {
            //            ServiceID = new Guid("13bd9e6d-3092-496b-8025-530f5f9c43de"),
            //            ServiceName = "Hire booth",
            //            Unit = "Minutes",
            //            Status = StatusUse.Available
            //        },
            //        new Service
            //        {
            //            ServiceID = new Guid("70a5a1fd-9c0b-4109-9638-5b6e63e71eca"),
            //            ServiceName = "Print photo",
            //            Unit = "Times",
            //            Status = StatusUse.Available
            //        });
        }
    }
}
