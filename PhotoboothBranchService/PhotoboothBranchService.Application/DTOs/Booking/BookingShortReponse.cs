using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class BookingShortReponse
    {
        public Guid BookingID { get; set; }
        public string CustomerBusinessID { get; set; } = default!;
        public long ValidateCode { get; set; }
        public decimal HireBoothFee { get; set; }
        public decimal TotalPrice { get; set; } = default!;
        public decimal PaidAmount { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingType BookingType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DateTime? CancelledDate { get; set; }
        public decimal RefundedAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid BoothID { get; set; }
        public Guid CustomerID { get; set; }
    }
}
