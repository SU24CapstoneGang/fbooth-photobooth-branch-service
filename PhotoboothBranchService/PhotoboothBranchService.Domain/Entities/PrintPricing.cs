namespace PhotoboothBranchService.Domain.Entities
{
    public class PrintPricing
    {
        public Guid PrintPricingID { get; set; }
        public string PrintName { get; set; }
        public string Description { get; set; }
        public float UnitPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual List<FinalPicture> FinalPictures { get; set; }
    }
}
