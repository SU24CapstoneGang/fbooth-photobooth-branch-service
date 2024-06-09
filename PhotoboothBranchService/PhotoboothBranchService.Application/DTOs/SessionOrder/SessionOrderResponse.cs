namespace PhotoboothBranchService.Application.DTOs.Session
{
    public class SessionOrderResponse
    {
        public Guid SessionID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid BranchesID { get; set; }
        public Guid PhotoBoothBranchId { get; set; }
        public Guid OrderId { get; set; }
    }
}
