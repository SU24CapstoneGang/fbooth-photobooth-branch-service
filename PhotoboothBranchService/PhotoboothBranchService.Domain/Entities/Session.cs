namespace PhotoboothBranchService.Domain.Entities
{
    public class Session
    {
        public Guid SessionID { get; set; } = default!;
        public int PhotosTaken { get; set; } = default!;
        public double TotalPrice { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
        public Guid BranchesID { get; set; }
        public virtual PhotoBoothBranch PhotoBoothBranch { get; set; }
        public Guid? DiscountID { get; set; }
        public virtual Discount Discount { get; set; }
        public Guid PrintPricingID { get; set; }
        public virtual PrintPricing PrintPricing { get; set; }
        public Guid? AccountID { get; set; }
        public virtual Account Account { get; set; }
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; }
        public virtual FinalPicture FinalPicture { get; set; }
        public virtual TransactionHistory TransactionHistory { get; set; }
    }
}
