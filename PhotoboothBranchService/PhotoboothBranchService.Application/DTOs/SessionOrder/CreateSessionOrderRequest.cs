namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class CreateSessionOrderRequest
    {
        public Guid BoothID { get; set; }
        public Guid AccountID { get; set; }
        public Guid ServiceID { get; set; }
        public DateTime? StartTime { get; set; } = default!;
    }
}
