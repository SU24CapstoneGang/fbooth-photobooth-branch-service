namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class UpdateServiceItemRequest
    {
        public short? Quantity { get; set; }
        public decimal? Price { get; set; }
        public Guid? ServiceID { get; set; }
        public Guid? SessionOrderID { get; set; }
    }
}
