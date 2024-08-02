using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Refund
{
    public class RefundRequest
    {
        public Guid transId { get; set; }
        public bool IsFullRefund { get; set; }
    }
}
