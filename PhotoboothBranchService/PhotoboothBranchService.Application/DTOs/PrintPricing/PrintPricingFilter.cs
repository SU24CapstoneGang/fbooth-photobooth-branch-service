namespace PhotoboothBranchService.Application.DTOs.PrintPricing
{
    public class PrintPricingFilter
    {
        public decimal? DiscountPerPrintNumber { get; set; }
        public int? MinQuantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
