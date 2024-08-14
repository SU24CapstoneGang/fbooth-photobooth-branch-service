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

            builder.Property(pm => pm.CreatedDate)
                .IsRequired();

            // Payment status enum mapping
            builder.Property(pm => pm.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentMethodStatus)Enum.Parse(typeof(PaymentMethodStatus), v));

            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')");

            builder.Property(c => c.LastModified)
                   .IsRequired()
                   .HasDefaultValueSql("(GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SE Asia Standard Time')")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(i => i.Payments)
                .WithOne(w => w.PaymentMethod)
                .HasForeignKey(i => i.PaymentMethodID)
                .IsRequired();

            //Add primordial data 
            builder.HasData(
                new PaymentMethod
                {
                    PaymentMethodID = new Guid("1b4f2a3e-7d94-4119-8b6d-5c15b02848f6"),
                    PaymentMethodName = "VNPay",
                    CreatedDate = DateTime.UtcNow,
                    Status = PaymentMethodStatus.Active,
                },
                new PaymentMethod
                {
                    PaymentMethodID = new Guid("f3b6e6b2-f90e-4f6b-8cd2-68b467afae0f"),
                    PaymentMethodName = "MoMo",
                    CreatedDate = DateTime.UtcNow,
                    Status = PaymentMethodStatus.Active,
                },
                new PaymentMethod
                {
                    PaymentMethodID = new Guid("0d82e0e5-ca54-4ff0-8750-e5ff77435584"),
                    PaymentMethodName = "Cash",
                    CreatedDate = DateTime.UtcNow,
                    Status = PaymentMethodStatus.Active,
                });
        }
    }
}
