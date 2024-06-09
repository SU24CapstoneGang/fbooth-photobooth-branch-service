namespace PhotoboothBranchService.Application.DTOs.Session
{
    public class CreateSessionOrderRequest
    {
        public int PhotosTaken { get; set; }
        public double TotalPrice { get; set; }
        public Guid BranchesID { get; set; }
        public Guid? AccountID { get; set; }
    }
}
