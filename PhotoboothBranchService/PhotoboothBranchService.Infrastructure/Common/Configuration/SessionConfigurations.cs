using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            // Table name
            builder.ToTable("Sessions");

            // Primary key
            builder.HasKey(s => s.SessionID);
            builder.Property(s => s.SessionID).HasColumnName("SessionID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(s => s.PhotosTaken)
            .IsRequired(); // Số ảnh đã chụp

            builder.Property(s => s.TotalPrice)
                .IsRequired(); // Tổng giá


            builder.Property(s => s.StartTime)
                .IsRequired();

            builder.Property(s => s.EndTime)
                .IsRequired();

            // Relationship with PhotoBoothBranch
            builder.HasOne(s => s.PhotoBoothBranch)
                .WithMany(pb => pb.Sessions)
                .HasForeignKey(s => s.BranchesID)
                .IsRequired();

            // Relationship with Layout
            builder.HasOne(s => s.Layout)
                .WithMany(l => l.Sessions)
                .HasForeignKey(s => s.LayoutID)
                .IsRequired();

            // Mối quan hệ một-nhieu giữa Session và Discount
            builder.HasOne(s => s.Discount)
                .WithMany(d => d.Sessions)
                .HasForeignKey(s => s.DiscountID)
                .IsRequired(false);

            // Mối quan hệ một-nhieu giữa Session và PrintPricing
            builder.HasOne(s => s.PrintPricing)
                .WithMany(p => p.Sessions)
                .HasForeignKey(s => s.PrintPricingID)
                .IsRequired();

            // Mối quan hệ một-nhieu giữa Session và Account
            builder.HasOne(s => s.Account)
                .WithMany(a => a.Sessions)
                .HasForeignKey(s => s.AccountID)
                .IsRequired(false);

            // Mối quan hệ một-một giữa Session và TransactionHistory
            builder.HasOne(s => s.TransactionHistory)
                .WithOne(th => th.Session)
                .HasForeignKey<TransactionHistory>(th => th.SessionID)
                .OnDelete(DeleteBehavior.Restrict);

            // Mối quan hệ một-một giữa Session và FinalPicture
            builder.HasOne(s => s.FinalPicture)
                .WithOne(fp => fp.Session)
                .HasForeignKey<FinalPicture>(fp => fp.SessionID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
