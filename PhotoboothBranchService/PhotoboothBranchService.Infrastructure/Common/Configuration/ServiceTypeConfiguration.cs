using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ServiceType> builder)
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
        }
    }
}
