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
    public class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            // Table name
            builder.ToTable("Sessions");

            // Primary key
            builder.HasKey(s => s.SessionID);
            builder.Property(s => s.SessionID).HasColumnName("Session ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(s => s.StartTime)
                .IsRequired();

            builder.Property(s => s.EndTime)
                .IsRequired();

            // Relationship with PhotoBoothBranch
            builder.HasOne(s => s.PhotoBoothBranch)
                .WithMany(pb => pb.Sessions)
                .HasForeignKey(s => s.BranchesID)
                .IsRequired();

            // Relationship with Order
            builder.HasOne(s => s.Order)
                .WithOne(o => o.Session)
                .HasForeignKey<Session>(s => s.SessionID)
                .IsRequired(false);
        }
    }
}
