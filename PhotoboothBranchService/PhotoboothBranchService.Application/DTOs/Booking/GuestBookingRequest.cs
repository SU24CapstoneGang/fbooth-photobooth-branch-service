using PhotoboothBranchService.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class GuestBookingRequest
    {
        [Required,EmailAddress(ErrorMessage = "Invalid Email format")]
        public string CustomerEmail { get; set; } = default!;
        [Required,RegularExpression(@"^0[1-9]\d{8}$", ErrorMessage = "Invalid PhoneNumber format")]
        public string CustomerPhoneNumber { get; set; } = default!;
        [Required, StringLength(30, ErrorMessage ="First name has max length is 30")]
        public string FirstName { get; set; } = default!;
        [Required, StringLength(30, ErrorMessage = "Last name has max length is 30")]
        public string LasttName { get; set; } = default!;
        [Required, StringLength(150, ErrorMessage = "Adress has max length is 150")]
        public string Address { get; set; } = default!;
        [Required]
        public DateOnly DateOfBirth { get; set; }
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
