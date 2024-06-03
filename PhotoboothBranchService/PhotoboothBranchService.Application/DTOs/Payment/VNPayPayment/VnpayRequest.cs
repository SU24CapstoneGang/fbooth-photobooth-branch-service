using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment
{
    public class VnpayRequest
    {
        public long Amount { get; set; }
        public string? Information { get; set; }
        public string? BankCode { get; set; }
        public Guid SessionID { get; set; }
        public string? OrderInformation { get; set; }
        public string? ClientIpAddress { get; set; }
    }
}
