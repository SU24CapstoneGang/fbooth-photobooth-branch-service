using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BookingServiceConfiguration : IEntityTypeConfiguration<BookingService>
    {
        public void Configure(EntityTypeBuilder<BookingService> builder)
        {
            builder.ToTable("BookingServices");
            builder.HasKey(s => s.BookingServiceID);
            builder.Property(s => s.BookingServiceID)
                .ValueGeneratedOnAdd();
            builder.Property(s => s.Quantity);
            builder.Property(s => s.Price).HasColumnType("decimal(18, 2)"); ;
            builder.Property(s => s.SubTotal).HasColumnType("decimal(18, 2)"); ;


            builder.HasOne(s => s.Service)
                .WithMany(a => a.BookingServices)
                .HasForeignKey(s => s.ServiceID)
                .IsRequired();

            builder.HasOne(s => s.SessionOrder)
                .WithMany(b => b.BookingServices)
                .HasForeignKey(c => c.SessionOrderID)
                .IsRequired();
        }
    }
}
