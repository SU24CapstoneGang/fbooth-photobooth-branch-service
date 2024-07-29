using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.FullPaymentPolicy
{
    public class CreatePolicyRequestModel
    {
        [Required(ErrorMessage = "Policy name is required.")]
        public string PolicyName { get; set; } = default!;

        [Required(ErrorMessage = "Policy description is required.")]
        public string PolicyDescription { get; set; } = default!;

        [Range(0, int.MaxValue, ErrorMessage = "Refund days before cannot be negative.")]
        public int RefundDaysBefore { get; set; } // Số ngày trước khi đặt phòng để được hoàn tiền

        public bool NoCheckInTimeLimit { get; set; } // Không bị giới hạn thời gian check in

        [DataType(DataType.Date, ErrorMessage = "StartDate must be a valid date.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateOnly? StartDate { get; set; } // Ngày bắt đầu áp dụng policy

        [DataType(DataType.Date, ErrorMessage = "EndDate must be a valid date.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateOnly? EndDate { get; set; } // Ngày kết thúc áp dụng policy, null nếu áp dụng vĩnh viễn
    }
}
