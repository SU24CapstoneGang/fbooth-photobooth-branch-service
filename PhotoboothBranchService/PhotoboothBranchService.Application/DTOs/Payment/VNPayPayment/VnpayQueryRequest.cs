using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment
{
    public class VnpayQueryRequest
    {
        public string SessionId { get; set; }
        public string PayDate { get; set; }
    }
}
