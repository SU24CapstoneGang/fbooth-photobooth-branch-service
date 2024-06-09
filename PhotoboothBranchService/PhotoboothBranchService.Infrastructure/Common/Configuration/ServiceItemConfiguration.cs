using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.Property(s => s.UnitPrice);

            builder.HasOne(s => s.Service)
                .WithMany(a => a.ServiceItems)
                .HasForeignKey(s => s.ServiceID)
                .IsRequired();
            builder.HasOne(s => s.PhotoSession)
                .WithMany(b => b.ServiceItems)
                .HasForeignKey(s => s.PhotoSessionID)
                .IsRequired();
        }
    }
}
