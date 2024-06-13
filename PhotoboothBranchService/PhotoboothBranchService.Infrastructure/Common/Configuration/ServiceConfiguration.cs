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
            builder.Property(a => a.Unit).IsRequired(); ;


            builder.HasOne(s => s.ServiceType)
                .WithMany(t => t.Services)
                .HasForeignKey(s => s.ServiceTypeID)
                .IsRequired();
            builder.HasMany(s => s.ServiceItems)
                .WithOne(i => i.Service)
                .HasForeignKey(i => i.ServiceID)
                .IsRequired();

        }
    }
}
