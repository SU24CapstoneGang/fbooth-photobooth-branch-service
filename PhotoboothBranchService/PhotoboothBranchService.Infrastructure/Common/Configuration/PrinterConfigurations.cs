using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

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
            builder.Property(p => p.PrinterID).HasColumnName("Printer ID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(p => p.ModelName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .IsRequired();

            // Status property
            builder.Property(p => p.Status)
                .IsRequired()
                .HasColumnType("int");

            // Relationship with PhotoBoothBranch
            builder.HasOne(p => p.PhotoBoothBranch)
                .WithOne(pb => pb.Printer)
                .HasForeignKey<Printer>(p => p.PhotoBoothBranchId)
                .IsRequired();
        }
    }
}
