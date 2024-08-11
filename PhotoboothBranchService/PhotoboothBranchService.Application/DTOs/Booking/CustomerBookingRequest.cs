using PhotoboothBranchService.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class CustomerBookingRequest
    {
        [Required]
        public Guid BoothID { get; set; }
        [DictionaryValueGreaterThanZero(ErrorMessage = "Each service quantity must be greater than 0.")]
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        [TimeSpanValidation]
        public TimeSpan StartTime { get; set; }
        [Required]
        [TimeSpanValidation]
        public TimeSpan EndTime { get; set; }
    }
}
