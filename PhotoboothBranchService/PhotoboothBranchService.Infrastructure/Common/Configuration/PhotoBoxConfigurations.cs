using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System.Reflection.Emit;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PhotoBoxConfigurations : IEntityTypeConfiguration<PhotoBox>
    {
        public void Configure(EntityTypeBuilder<PhotoBox> builder)
        {
            builder.ToTable("PhotoBox");
            builder.HasKey(u => u.PhotoBoxID);
            builder.Property(u => u.PhotoBoxID)
                .HasColumnName("PhotoBoxID")
                .ValueGeneratedOnAdd();
            builder.Property(u => u.BoxWidth).IsRequired();
            builder.Property(u => u.BoxHeight).IsRequired();
            builder.Property(u => u.CoordinatesX).IsRequired();
            builder.Property(u => u.CoordinatesY).IsRequired();
            builder.Property(u => u.IsLandscape).IsRequired();
            builder.Property(u => u.BoxIndex).IsRequired();

            builder.HasData(
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("74f5f643-3a94-402d-b139-3b2543255f7c"),
                        BoxHeight = 2631,
                        BoxWidth = 3921,
                        CoordinatesX = 190,
                        CoordinatesY = 170,
                        IsLandscape = false,
                        BoxIndex = 0,
                        LayoutID = Guid.Parse("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("8e69c79f-09ee-495e-861e-0c60207144d7"),
                        BoxHeight = 1641,
                        BoxWidth = 2111,
                        CoordinatesX = 190,
                        CoordinatesY = 2940,
                        IsLandscape = false,
                        BoxIndex = 1,
                        LayoutID = Guid.Parse("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("1e1c3d0b-b7d3-4dc8-9ebd-758f44fef53a"),
                        BoxHeight = 1641,
                        BoxWidth = 2111,
                        CoordinatesX = 2480,
                        CoordinatesY = 2940,
                        IsLandscape = false,
                        BoxIndex = 2,
                        LayoutID = Guid.Parse("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("170db2dc-bfa8-4148-b85e-75b52a72b5d4"),
                        BoxHeight = 1641,
                        BoxWidth = 2101,
                        CoordinatesX = 4780,
                        CoordinatesY = 2940,
                        IsLandscape = false,
                        BoxIndex = 3,
                        LayoutID = Guid.Parse("2920f5b2-2cbf-4ed3-7ff3-08dca62a5d8b")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("5fc689f6-8890-43a6-9d7d-59a9c5e10bc3"),
                        BoxHeight = 5080,
                        BoxWidth = 3460,
                        CoordinatesX = 160,
                        CoordinatesY = 340,
                        IsLandscape = false,
                        BoxIndex = 0,
                        LayoutID = Guid.Parse("1306f740-c718-41bb-d91c-08dca62b7b83")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("c98c89af-738e-4034-88a2-9a8982fc22d5"),
                        BoxHeight = 2370,
                        BoxWidth = 3460,
                        CoordinatesX = 160,
                        CoordinatesY = 5680,
                        IsLandscape = false,
                        BoxIndex = 1,
                        LayoutID = Guid.Parse("1306f740-c718-41bb-d91c-08dca62b7b83")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("56d05b8f-6cf4-42f2-a9aa-7d96065b4c94"),
                        BoxHeight = 2370,
                        BoxWidth = 3460,
                        CoordinatesX = 3480,
                        CoordinatesY = 5680,
                        IsLandscape = false,
                        BoxIndex = 2,
                        LayoutID = Guid.Parse("1306f740-c718-41bb-d91c-08dca62b7b83")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("23cb58a4-d569-414d-93f6-96c3e6c60614"),
                        BoxHeight = 3170,
                        BoxWidth = 4430,
                        CoordinatesX = 1830,
                        CoordinatesY = 1630,
                        IsLandscape = false,
                        BoxIndex = 0,
                        LayoutID = Guid.Parse("4a071101-a8fd-42dd-d91d-08dca62b7b83")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("54e510a7-d789-4cf5-ae64-025c38a2ff58"),
                        BoxHeight = 1370,
                        BoxWidth = 4430,
                        CoordinatesX = 1830,
                        CoordinatesY = 5100,
                        IsLandscape = false,
                        BoxIndex = 1,
                        LayoutID = Guid.Parse("4a071101-a8fd-42dd-d91d-08dca62b7b83")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("7cb6bbf5-2fef-47e1-9c28-82b9c599fdd0"),
                        BoxHeight = 1370,
                        BoxWidth = 4430,
                        CoordinatesX = 1830,
                        CoordinatesY = 6810,
                        IsLandscape = false,
                        BoxIndex = 2,
                        LayoutID = Guid.Parse("4a071101-a8fd-42dd-d91d-08dca62b7b83")
                    },
                    new PhotoBox
                    {
                        PhotoBoxID = Guid.Parse("832d5af1-9897-4a6d-a98a-77443df1b4e2"),
                        BoxHeight = 3686,
                        BoxWidth = 5529,
                        CoordinatesX = 1000,
                        CoordinatesY = 1022,
                        IsLandscape = false,
                        BoxIndex = 0,
                        LayoutID = Guid.Parse("1799860e-e239-47c6-c5a1-08dca65d7432")
                    });

        }
    }
}
