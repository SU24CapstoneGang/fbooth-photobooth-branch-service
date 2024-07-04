using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class SessionPackageConfiguration : IEntityTypeConfiguration<SessionPackage>
    {
        public void Configure(EntityTypeBuilder<SessionPackage> builder)
        {
            builder.ToTable("SessionPackages");
            // Primary key
            builder.HasKey(u => u.SessionPackageID);
            builder.Property(u => u.SessionPackageID).HasColumnName("SessionPackageID").ValueGeneratedOnAdd();
            builder.Property(b => b.SessionPackageName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.SessionPackageDescription).IsRequired().HasMaxLength(250);
            builder.Property(b => b.Price).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(b => b.Duration).IsRequired();
            builder.Property(b => b.EmailSendCount).IsRequired();
            builder.Property(b => b.PrintCount).IsRequired();

            builder.HasMany(k => k.SessionOrders)
                .WithOne(i => i.SessionPackage)
                .HasForeignKey(i => i.SessionPackageID)
                .IsRequired();

        }
    }
}
