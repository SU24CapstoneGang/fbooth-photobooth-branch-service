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

            builder.HasData(new SessionPackage
            {
                SessionPackageID = new Guid("306c5339-f453-4e2d-839e-4b3aacf17084"),
                Duration = 90,
                EmailSendCount = 10,
                PrintCount = 10,
                SessionPackageName = "90 minutes mornal package",
                SessionPackageDescription = "Package have 90 minutes in take photo, can print 10 photo and seend to email 10 times",
                Price = 310000
            },
            new SessionPackage
            {
                SessionPackageID = new Guid("318dc257-983e-45d7-8dcb-10f348975c38"),
                Duration = 90,
                EmailSendCount = 20,
                PrintCount = 20,
                SessionPackageName = "90 minutes special package",
                SessionPackageDescription = "Package have 90 minutes in take photo, can print 20 photo and seend to email 20 times",
                Price = 440000
            },
            new SessionPackage
            {
                SessionPackageID = new Guid("6c739b97-36d4-4559-8738-de8cc132b705"),
                Duration = 90,
                EmailSendCount = 50,
                PrintCount = 50,
                SessionPackageName = "90 minutes special package PROMAX",
                SessionPackageDescription = "Package have 90 minutes in take photo, can print 50 photo and seend to email 50 times",
                Price = 830000
            },
            new SessionPackage
            {
                SessionPackageID = new Guid("f98b3003-c0ba-4305-a5ce-d8f77dd7310e"),
                Duration = 60,
                EmailSendCount = 10,
                PrintCount = 10,
                SessionPackageName = "60 minutes normal package ",
                SessionPackageDescription = "Package have 60 minutes in take photo, can print 10 photo and seend to email 10 times",
                Price = 310000
            },
            new SessionPackage
            {
                SessionPackageID = new Guid("2fbb7633-5796-465a-98a6-f3c484811f24"),
                Duration = 60,
                EmailSendCount = 20,
                PrintCount = 20,
                SessionPackageName = "60 minutes special package ",
                SessionPackageDescription = "Package have 60 minutes in take photo, can print 20 photo and seend to email 20 times",
                Price = 440000
            },
            new SessionPackage
            {
                SessionPackageID = new Guid("041f73b6-7c08-4c63-8a08-aaebae219048"),
                Duration = 150,
                EmailSendCount = 50,
                PrintCount = 50,
                SessionPackageName = "150 minutes special package ",
                SessionPackageDescription = "Package have 150 minutes in take photo, can print 50 photo and seend to email 50 times",
                Price = 950000
            });
        }
    }
}
