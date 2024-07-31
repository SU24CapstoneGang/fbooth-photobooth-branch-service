using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.BookingService
{
    public class CreateBookingServiceRequest
    {
        [Range(1, 100, ErrorMessage = "Quantity must greater than 0")]
        public short Quantity { get; set; }
        public Guid ServiceID { get; set; }
        public Guid BookingID { get; set; }
    }
}
