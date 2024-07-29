using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration
{
    public class RefundConfigurations : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable("Refunds");
            builder.HasKey(r => r.RefundID);
            builder.Property(r => r.RefundID).ValueGeneratedOnAdd();
            builder.Property(r => r.RefundDateTime);
            builder.Property(r => r.Amount);
            builder.Property(r => r.Description);
            builder.Property(r => r.Status);
        }
    }
}
