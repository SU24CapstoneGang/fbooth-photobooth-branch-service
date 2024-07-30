namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class BookingServiceResponse
    {
        public Guid ServiceItemID { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public Guid ServiceID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
