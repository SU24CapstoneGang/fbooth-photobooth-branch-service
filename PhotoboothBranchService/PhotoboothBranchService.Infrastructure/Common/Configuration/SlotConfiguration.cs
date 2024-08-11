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
    public class SlotConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.ToTable("Slots");
            builder.HasKey(i => i.SlotID);
            builder.Property(i => i.SlotID).ValueGeneratedOnAdd().IsRequired();
            builder.Property(i => i.SlotStartTime).IsRequired();
            builder.Property(i => i.SlotEndTime).IsRequired();
            builder.Property(i => i.Status).IsRequired();
            builder.Property(i => i.Price).IsRequired().HasColumnType("decimal(18, 0)");
            builder.HasMany(i => i.BookingSlots)
                .WithOne(a => a.Slot)
                .HasForeignKey(a => a.SlotID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
