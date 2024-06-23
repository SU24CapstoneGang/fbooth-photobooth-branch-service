using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ServiceItemConfiguration : IEntityTypeConfiguration<ServiceItem>
    {
        public void Configure(EntityTypeBuilder<ServiceItem> builder)
        {
            builder.ToTable("ServiceItems");
            builder.HasKey(s => s.ServiceItemID);
            builder.Property(s => s.ServiceItemID)
                .HasColumnName("ServiceItemID")
                .ValueGeneratedOnAdd();
            builder.Property(s => s.Quantity);
            builder.Property(s => s.UnitPrice).HasColumnType("decimal(18, 2)"); ;
            builder.Property(s => s.SubTotal).HasColumnType("decimal(18, 2)"); ;


            builder.HasOne(s => s.Service)
                .WithMany(a => a.ServiceItems)
                .HasForeignKey(s => s.ServiceID)
                .IsRequired();

            builder.HasOne(s => s.PhotoSession)
                .WithMany(b => b.ServiceItems)
                .HasForeignKey(s => s.PhotoSessionID)
                .IsRequired(false);

            builder.HasOne(s => s.SessionOrder)
                .WithMany(b => b.ServiceItems)
                .HasForeignKey(c => c.SessionOrderID)
                .IsRequired();
        }
    }
}
