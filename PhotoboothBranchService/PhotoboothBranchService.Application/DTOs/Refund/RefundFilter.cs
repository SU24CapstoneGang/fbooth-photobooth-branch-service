using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Refund
{
    public class RefundFilter
    {
        public string? Description { get; set; }
        public RefundStatus? Status { get; set; }
        public Guid? TransactionID { get; set; }
    }
}
