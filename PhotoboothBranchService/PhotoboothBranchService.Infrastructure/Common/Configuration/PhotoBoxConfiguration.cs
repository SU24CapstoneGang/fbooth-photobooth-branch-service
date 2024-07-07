using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PhotoBoxConfiguration : IEntityTypeConfiguration<PhotoBox>
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
        }
    }
}
