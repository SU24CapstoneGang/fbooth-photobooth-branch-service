using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment
{
    public class VnpayRefundRequest
    {
        public string RefundCategory { get; set; }
        public string Amount { get; set; }
        public string SessionId { get; set; }
        public string TransId { get; set; }
        public string PayDate { get; set; }
        public string User { get; set; }
    }
}
