using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Enum
{
    public enum TransactionStatus
    {
        Success = 1,
        Fail = 0,
        Processing = 2,
        RefundedFull = 3,
        RefundedPartial = 4,
        Redundant = 5,
    }
}
