using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class UpdateBookingRequest
    {
        [Required]
        public Guid BoothID { get; set; }
        [Required]
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
