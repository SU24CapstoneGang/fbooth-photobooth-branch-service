using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System.Reflection.Emit;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class LayoutConfigurations : IEntityTypeConfiguration<Layout>
    {
        public void Configure(EntityTypeBuilder<Layout> builder)
        {
            // Table name
            builder.ToTable("Layouts");

            // Primary key
            builder.HasKey(l => l.LayoutID);
            builder.Property(l => l.LayoutID).HasColumnName("LayoutID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(l => l.LayoutCode).IsRequired();
            builder.Property(l => l.LayoutURL).IsRequired();
            builder.Property(l => l.CouldID).IsRequired();
            builder.Property(l => l.Height).IsRequired();
            builder.Property(l => l.Width).IsRequired();
            builder.Property(l => l.PhotoSlot).IsRequired();

            // ManufactureStatus enum mapping
            builder.Property(l => l.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusUse)Enum.Parse(typeof(StatusUse), v));

            builder.Property(l => l.LastModified);

            //auto add CreateDate
            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(t => t.PhotoSessions)
                .WithOne(p => p.Layout)
                .HasForeignKey(p => p.LayoutID)
                .IsRequired(false);

            builder.HasMany(t => t.PhotoBoxes)
                .WithOne(p => p.Layout)
                .HasForeignKey(v => v.LayoutID)
                .IsRequired();

            builder.HasMany(i => i.Backgrounds)
                .WithOne(p => p.Layout)
                .HasForeignKey(pt => pt.LayoutID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasData(
                    new Layout
                    {
                        LayoutID = Guid.Parse("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b"),
                        LayoutCode = "11-01",
                        LayoutURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721198261/FBooth-Layout/frtwksfwbszafz8c7h3b.png",
                        CouldID = "FBooth-Layout/frtwksfwbszafz8c7h3b",
                        Status = StatusUse.Available,
                        Height = 4730,
                        Width = 7090,
                        PhotoSlot = 4,
                        CreatedDate = DateTime.Parse("2024-07-17T13:37:40.75"),
                        LastModified = null
                    },
                    new Layout
                    {
                        LayoutID = Guid.Parse("1306f740-c718-41bb-d91c-08dca62b7b83"),
                        LayoutCode = "7-01",
                        LayoutURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721198486/FBooth-Layout/fwuzc2ba67ysibm5gccj.png",
                        CouldID = "FBooth-Layout/fwuzc2ba67ysibm5gccj",
                        Status = StatusUse.Available,
                        Height = 7100,
                        Width = 4730,
                        PhotoSlot = 3,
                        CreatedDate = DateTime.Parse("2024-07-17T13:41:26.77"),
                        LastModified = null
                    },
                    new Layout
                    {
                        LayoutID = Guid.Parse("4a071101-a8fd-42dd-d91d-08dca62b7b83"),
                        LayoutCode = "9-01",
                        LayoutURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721198502/FBooth-Layout/gjes09uywe0uv3veuz8s.png",
                        CouldID = "FBooth-Layout/gjes09uywe0uv3veuz8s",
                        Status = StatusUse.Available,
                        Height = 4730,
                        Width = 7090,
                        PhotoSlot = 3,
                        CreatedDate = DateTime.Parse("2024-07-17T13:41:42.85"),
                        LastModified = null
                    },
                    new Layout
                    {
                        LayoutID = Guid.Parse("1799860e-e239-47c6-c5a1-08dca65d7432"),
                        LayoutCode = "4-01",
                        LayoutURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721219948/FBooth-Layout/d2rcoxdzla53ngpzarv9.png",
                        CouldID = "FBooth-Layout/d2rcoxdzla53ngpzarv9",
                        Status = StatusUse.Available,
                        Height = 4730,
                        Width = 7090,
                        PhotoSlot = 1,
                        CreatedDate = DateTime.Parse("2024-07-17T19:39:09.34"),
                        LastModified = null
                    },
                    new Layout
                    {
                        LayoutID = Guid.Parse("e1bb7b30-909b-491a-7f64-08dcaa6cbb13"),
                        LayoutCode = "Black Vintage Photo Film Your Story (3)",
                        LayoutURL = "https://res.cloudinary.com/dfxvccyje/image/upload/v1721666315/FBooth-Layout/hucotkisjh7y94sirnza.png",
                        CouldID = "FBooth-Layout/hucotkisjh7y94sirnza",
                        Status = StatusUse.Available,
                        Height = 1920,
                        Width = 1080,
                        PhotoSlot = 1,
                        CreatedDate = DateTime.Parse("2024-07-22T23:38:35.1833333"),
                        LastModified = null
                    });
        }
    }
}
