using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BoothConfiguration : IEntityTypeConfiguration<Booth>
    {
        public void Configure(EntityTypeBuilder<Booth> builder)
        {
            builder.ToTable("Booths");
            // Primary key
            builder.HasKey(u => u.BoothID);
            builder.Property(u => u.BoothID).HasColumnName("BoothID").ValueGeneratedOnAdd();
            builder.Property(b => b.BoothName).IsRequired().HasMaxLength(50);

            // ManufactureStatus enum mapping
            builder.Property(pb => pb.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ManufactureStatus)Enum.Parse(typeof(ManufactureStatus), v));

            builder.HasMany(s => s.PhotoSessions)
                .WithOne(b => b.Booth)
                .HasForeignKey(b => b.BoothID)
                .IsRequired();
            builder.HasMany(s => s.SessionOrders)
                .WithOne(a => a.Booth)
                .HasForeignKey(c => c.BoothID)
                .IsRequired();
        }
    }
}
