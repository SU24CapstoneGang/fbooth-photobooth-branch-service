using PhotoboothBranchService.Application.DTOs.Refund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class CancelBookingResponse
    {
        public bool isSuccess {  get; set; } = false;
        public string message { get; set; } = "";
        public bool isRefund {  get; set; } = false;
        public List<RefundResponse> refundList { get; set; }
    }
}
