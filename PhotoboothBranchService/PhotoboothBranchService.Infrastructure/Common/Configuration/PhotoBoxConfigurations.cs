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

        }
    }
}
