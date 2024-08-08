using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Enum
{
    public enum PaymentStatus
    {
        Fail = 0,
        Processing = 1,
        Refunded = 2,
        Paid = 3,
        PendingPayExtra = 4
    }
}
