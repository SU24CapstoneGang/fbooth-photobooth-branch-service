using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.FullPaymentPolicy
{
    public class FullPaymentPolicyFilter
    {
        public Guid? FullPaymentPolicyID { get; set; }
        public string? PolicyName { get; set; } = default!;
        public string? PolicyDescription { get; set; } = default!;
        public int? RefundDaysBefore { get; set; } // Số ngày trước khi đặt phòng để được hoàn tiền
        public bool? NoCheckInTimeLimit { get; set; } // Không bị giới hạn thời gian check in
        public bool? IsActive { get; set; } // Trạng thái của chính sách, áp dụng hoặc không áp dụng

        public DateOnly? CreatedDate { get; set; } 
        public DateOnly? StartDate { get; set; } // Ngày bắt đầu áp dụng policy
        public DateOnly? EndDate { get; set; } // Ngày kết thúc áp dụng policy, null nếu áp dụng vĩnh viễn

        public bool? IsDefaultPolicy { get; set; } //  Ngày bắt đầu áp dụng policy null thì chính sách mặc định luôn được áp dụng
        public bool? IsPermanentPolicy { get; set; } // Ngày kết thúc áp dụng policy null thì áp dụng vĩnh viễn
    }
}
