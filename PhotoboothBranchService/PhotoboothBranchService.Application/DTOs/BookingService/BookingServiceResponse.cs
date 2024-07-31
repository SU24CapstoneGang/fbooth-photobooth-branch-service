using PhotoboothBranchService.Application.DTOs.Service;

namespace PhotoboothBranchService.Application.DTOs.BookingService
{
    public class BookingServiceResponse
    {
        public Guid BookingServiceID { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public ServiceResponse Service { get; set; }
    }
}
