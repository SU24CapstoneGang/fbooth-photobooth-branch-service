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

         
        }
    }
}
