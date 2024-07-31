namespace PhotoboothBranchService.Application.DTOs.BookingService
{
    public class CreateBookingServiceResponse
    {
        public Guid BookingServiceID { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public Guid ServiceID { get; set; }
        public Guid BookingID { get; set; }
    }
}
