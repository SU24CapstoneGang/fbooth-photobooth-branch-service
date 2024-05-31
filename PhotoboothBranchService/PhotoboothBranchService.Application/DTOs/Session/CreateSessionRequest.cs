namespace PhotoboothBranchService.Application.DTOs.Session
{
    public class CreateSessionRequest
    {
        public int PhotosTaken { get; set; }
        public double TotalPrice { get; set; }
        public Guid BranchesID { get; set; }
        public Guid? DiscountID { get; set; }
        public Guid PrintPricingID { get; set; }
        public Guid? AccountID { get; set; }
    }
}
