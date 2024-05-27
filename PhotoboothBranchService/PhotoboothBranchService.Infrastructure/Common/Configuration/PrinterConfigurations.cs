using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PrinterConfigurations : IEntityTypeConfiguration<Printer>
    {
        public void Configure(EntityTypeBuilder<Printer> builder)
        {
            // Table name
            builder.ToTable("Printers");

            // Primary key
            builder.HasKey(p => p.PrinterID);
            builder.Property(p => p.PrinterID).HasColumnName("PrinterID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(p => p.ModelName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .IsRequired();

            // Status enum mapping
            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ManufactureStatus)Enum.Parse(typeof(ManufactureStatus), v));

            // Relationship with PhotoBoothBranch
            builder
               .HasOne(p => p.PhotoBoothBranch)
               .WithOne(pb => pb.Printer)
               .HasForeignKey<PhotoBoothBranch>(pb => pb.PrinterID)
               .IsRequired(false);
        }
    }
}
