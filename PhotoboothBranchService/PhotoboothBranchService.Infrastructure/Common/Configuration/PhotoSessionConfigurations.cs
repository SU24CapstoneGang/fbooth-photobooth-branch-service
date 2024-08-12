using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PhotoSessionConfigurations : IEntityTypeConfiguration<PhotoSession>
    {
        public void Configure(EntityTypeBuilder<PhotoSession> builder)
        {
            builder.ToTable("PhotoSessions");
            // Primary key
            builder.HasKey(u => u.PhotoSessionID);
            builder.Property(u => u.PhotoSessionID).HasColumnName("PhotoSessionID").ValueGeneratedOnAdd();

            builder.Property(u => u.SessionName).IsRequired();
            builder.Property(u => u.SessionIndex).IsRequired();
            builder.Property(u => u.TotalPhotoTaken).IsRequired(false);
            builder.Property(u => u.Status).IsRequired();
            builder.Property(a => a.StartTime)
              .ValueGeneratedOnAdd()
              .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");
            builder.Property(u => u.EndTime).IsRequired(false);

            builder.HasMany(a => a.Photos)
                .WithOne(b => b.PhotoSession)
                .HasForeignKey(b => b.PhotoSessionID)
                .IsRequired();

        }
    }
}
