namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class CreateSessionOrderRequest
    {
        public double? TotalPrice { get; set; }
        public Guid PhotoBoothBranchID { get; set; }
        public Guid BoothID { get; set; }
        public Guid AccountID { get; set; }
    }
}
