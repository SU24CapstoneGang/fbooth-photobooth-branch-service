using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System.Reflection.Emit;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class BackgroundConfigurations : IEntityTypeConfiguration<Background>
    {
        public void Configure(EntityTypeBuilder<Background> builder)
        {
            // Table name
            builder.ToTable("Backgrounds");

            // Primary key
            builder.HasKey(f => f.BackgroundID);
            builder.Property(f => f.BackgroundID).HasColumnName("BackgroundID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(f => f.BackgroundCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.BackgroundURL).IsRequired();
            builder.Property(f => f.CouldID).IsRequired();
            builder.Property(f => f.CreatedDate).IsRequired();
            builder.Property(f => f.Height).IsRequired();
            builder.Property(f => f.Width).IsRequired();

            //auto add CreateDate
            builder.Property(c => c.CreatedDate)
                  .IsRequired()
                  .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            // Status enum mapping
            builder.Property(ep => ep.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));

            builder.HasMany(p => p.Photos)
                .WithOne(f => f.Background)
                .HasForeignKey(f => f.BackgroundID)
            .IsRequired(false);

            builder.HasData(
                    new Background
                    {
                        BackgroundID = Guid.Parse("fd3d39c7-62c6-4e4e-92a8-4e22a911b511"),
                        BackgroundCode = "9-01.png",
                        BackgroundURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666194/FBooth-Background/wnnwcfvfdqaqiih3rikm.png",
                        CouldID = "FBooth-Background/wnnwcfvfdqaqiih3rikm",
                        Height = 473,
                        Width = 709,
                        CreatedDate = DateTime.Parse("2024-07-22T23:36:34.2966667"),
                        LastModified = null,
                        LayoutID = Guid.Parse("4a071101-a8fd-42dd-d91d-08dca62b7b83"),
                        Status = StatusUse.Available
                    },
                    new Background
                    {
                        BackgroundID = Guid.Parse("867f6d24-6e2e-4fbd-a718-4c2c891fd826"),
                        BackgroundCode = "4-01.png",
                        BackgroundURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666311/FBooth-Background/c1jbpjf0xdw4lysrnrbm.png",
                        CouldID = "FBooth-Background/c1jbpjf0xdw4lysrnrbm",
                        Height = 473,
                        Width = 709,
                        CreatedDate = DateTime.Parse("2024-07-22T23:36:38.8466667"),
                        LastModified = null,
                        LayoutID = Guid.Parse("1799860e-e239-47c6-c5a1-08dca65d7432"),
                        Status = StatusUse.Available
                    },
                    new Background
                    {
                        BackgroundID = Guid.Parse("7df5c376-e2c7-47e5-bd86-558b7c7b1fd0"),
                        BackgroundCode = "7-01.png",
                        BackgroundURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666189/FBooth-Background/hughd2ixfiieefwniuwk.png",
                        CouldID = "FBooth-Background/hughd2ixfiieefwniuwk",
                        Height = 710,
                        Width = 473,
                        CreatedDate = DateTime.Parse("2024-07-22T23:36:22.3166667"),
                        LastModified = null,
                        LayoutID = Guid.Parse("1306f740-c718-41bb-d91c-08dca62b7b83"),
                        Status = StatusUse.Available
                    },
                    new Background
                    {
                        BackgroundID = Guid.Parse("b15f7e3c-ec3f-4f37-9a4d-4693bd0fa05f"),
                        BackgroundCode = "11-01.png",
                        BackgroundURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666183/FBooth-Background/jqdwh9a2y5vct3qunu7a.png",
                        CouldID = "FBooth-Background/jqdwh9a2y5vct3qunu7a",
                        Height = 473,
                        Width = 709,
                        CreatedDate = DateTime.Parse("2024-07-22T23:36:28.2166667"),
                        LastModified = null,
                        LayoutID = Guid.Parse("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b"),
                        Status = StatusUse.Available
                    });

        }
    }
}
