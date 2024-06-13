using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PhotoSessionConfiguration : IEntityTypeConfiguration<PhotoSession>
    {
        public void Configure(EntityTypeBuilder<PhotoSession> builder)
        {
            builder.ToTable("PhotoSessions");
            // Primary key
            builder.HasKey(u => u.PhotoSessionID);
            builder.Property(u => u.PhotoSessionID).HasColumnName("PhotoSessionID").ValueGeneratedOnAdd();

            builder.Property(u => u.SessionIndex).IsRequired();
            builder.Property(u => u.TotalPhotoTaken).IsRequired();

            builder.Property(a => a.StartTime)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
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
                .HasForeignKey(c=>c.BoothID)
                .IsRequired();
        }
    }
}
