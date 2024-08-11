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
    public class BookingSlotConfiguration : IEntityTypeConfiguration<BookingSlot>
    {
        public void Configure(EntityTypeBuilder<BookingSlot> builder)
        {
            builder.ToTable("BookingSlots");
            builder.HasKey(b => b.BookingSlotID);
            builder.Property(b => b.BookingSlotID).ValueGeneratedOnAdd().IsRequired();
            builder.Property(b => b.BookingDate).IsRequired();
            builder.Property(b => b.Price).IsRequired().HasColumnType("decimal(18, 0)");
        }
    }
}
