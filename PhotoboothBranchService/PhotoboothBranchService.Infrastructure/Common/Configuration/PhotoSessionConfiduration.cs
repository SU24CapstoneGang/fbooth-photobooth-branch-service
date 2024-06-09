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
    public class PhotoSessionConfiduration : IEntityTypeConfiguration<PhotoSession>
    {
        public void Configure(EntityTypeBuilder<PhotoSession> builder)
        {
            builder.ToTable("PhotoSessions");
            // Primary key
            builder.HasKey(u => u.PhotoSessionID);
            builder.Property(u => u.PhotoSessionID).HasColumnName("PhotoSessionID").ValueGeneratedOnAdd();

            builder.Property(u => u.StartTime).IsRequired();
            builder.Property(u => u.EndTime).IsRequired(false);

            builder.HasMany(a => a.Photos)
                .WithOne(b => b.PhotoSession)
                .HasForeignKey(b => b.PhotoSessionID)
                .IsRequired();

            builder.HasMany(b => b.ServiceItems)
                .WithOne(a => a.PhotoSession)
                .HasForeignKey(a => a.PhotoSessionID)
                .IsRequired();

            builder.HasOne(a=>a.Booth)
                .WithMany(b=>b.PhotoSessions)
                .HasForeignKey(a=>a.SessionOrderID)
                .IsRequired();

            builder.HasOne(a => a.SessionOrder)
                .WithMany(b => b.PhotoSessions)
                .HasForeignKey(a => a.SessionOrderID)
                .IsRequired();
        }
    }
}
