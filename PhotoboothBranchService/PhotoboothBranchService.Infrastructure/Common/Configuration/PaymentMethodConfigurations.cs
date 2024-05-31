using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class PaymentMethodConfigurations : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            // Table name
            builder.ToTable("PaymentMethods");

            // Primary key
            builder.HasKey(pm => pm.PaymentMethodID);
            builder.Property(pm => pm.PaymentMethodID).HasColumnName("PaymentMethodID")
                .ValueGeneratedOnAdd();

            // Other properties
            builder.Property(pm => pm.PaymentMethodName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pm => pm.CreateDate)
                .IsRequired();

            // Payment status enum mapping
            builder.Property(pm => pm.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));

            // Mối quan hệ một-nhiều giữa PaymentMethod và TransactionHistory
            builder.HasMany(pm => pm.TransactionHistories)
            .WithOne(th => th.PaymentMethod)
            .HasForeignKey(th => th.PaymentMethodID)
            .IsRequired();

            //auto add CreateDate and ignore change after update
            builder.Property(a => a.CreateDate)
              .ValueGeneratedOnAdd()
              .HasDefaultValue(DateTime.UtcNow)
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            ////Add primordial data
            //builder.HasData(
            //    new PaymentMethod
            //    {
            //        PaymentMethodID = new Guid("3ff27515-80e9-4ea9-8a60-5e2b02bc338a"),
            //        PaymentMethodName = "VNPay",
            //        CreateDate = DateTime.UtcNow,
            //        Status = PaymentStatus.Active,
            //    },
            //    new PaymentMethod
            //    {
            //        PaymentMethodID = new Guid("e7292894-9c01-40f0-8354-e974d93098b7"),
            //        PaymentMethodName = "MoMo",
            //        CreateDate = DateTime.UtcNow,
            //        Status = PaymentStatus.Active,
            //    });
        }
    }
}
