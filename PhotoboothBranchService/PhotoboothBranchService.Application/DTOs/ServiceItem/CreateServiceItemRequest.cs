namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class CreateServiceItemRequest
    {
        public short? Quantity { get; set; }
        public Guid? PhotoSessionID { get; set; }
        public Guid ServiceID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
