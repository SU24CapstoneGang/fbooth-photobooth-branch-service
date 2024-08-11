using PhotoboothBranchService.Application.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class BookingRequest
    {
        [Required]
        public Guid BoothID { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string? CustomerEmail { get; set; }
        [RegularExpression(@"^0[1-9]\d{8}$", ErrorMessage = "Invalid PhoneNumber format")]
        public string? CustomerPhoneNumber { get; set; }
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
