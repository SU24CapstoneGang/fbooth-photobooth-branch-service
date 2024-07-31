using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class BookingServiceResponse
    {
        public Guid BookingServiceID { get; set; }
        public string ServiceName { get; set; } = default!;
        public short Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public Guid ServiceID { get; set; }
        public Service Service { get; set; }
    }
}
