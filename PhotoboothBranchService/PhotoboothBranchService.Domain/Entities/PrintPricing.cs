namespace PhotoboothBranchService.Domain.Entities
{
    public class PrintPricing
    {
        public Guid PrintPricingID { get; set; }
        public string PrintName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public float UnitPrice { get; set; }
        public int MinQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual List<Session> Sessions { get; set; }
    }
}
