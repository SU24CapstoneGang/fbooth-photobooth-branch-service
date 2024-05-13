﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration
{
    public class PrinterConfigurations : IEntityTypeConfiguration<Printer>
    {
        public void Configure(EntityTypeBuilder<Printer> builder)
        {
            // Table name
            builder.ToTable("Printers");

            // Primary key
            builder.HasKey(p => p.PrinterID);

            // Other properties
            builder.Property(p => p.ModelName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .IsRequired();

            // Relationship with PhotoBoothBranch
            builder.HasOne(p => p.PhotoBoothBranch)
                .WithOne(pb => pb.Printer)
                .HasForeignKey<Printer>(p => p.PhotoBoothBranchId)
                .IsRequired();
        }
    }
}
