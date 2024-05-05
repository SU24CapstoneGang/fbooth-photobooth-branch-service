using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Configuration;

public class PrintersConfiguration : IEntityTypeConfiguration<Printers>
{
    public void Configure(EntityTypeBuilder<Printers> builder)
    {
        builder.ToTable("Printers");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ModelName).HasMaxLength(255).IsRequired();
        builder.Property(p => p.Lens).HasMaxLength(255);

        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<int>(); // Convert enum to int for storage
    }
}
