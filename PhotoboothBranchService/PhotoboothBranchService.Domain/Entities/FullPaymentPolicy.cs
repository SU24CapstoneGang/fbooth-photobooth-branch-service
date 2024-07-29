using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class FullPaymentPolicy
    {
        public Guid FullPaymentPolicyID { get; set; }
        public string PolicyName { get; set; } = default!;
        public string PolicyDescription { get; set; } = default!;
        public int RefundDaysBefore { get; set; } // Số ngày trước khi đặt phòng để được hoàn tiền
        public bool NoCheckInTimeLimit { get; set; } // Không bị giới hạn thời gian check in
        public bool IsActive { get; set; } // Trạng thái của chính sách, áp dụng hoặc không áp dụng

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? StartDate { get; set; } // Ngày bắt đầu áp dụng policy
        public DateTime? EndDate { get; set; } // Ngày kết thúc áp dụng policy, null nếu áp dụng vĩnh viễn

        public virtual ICollection<Booking> Bookings { get; set; } = default!;
    }
}
