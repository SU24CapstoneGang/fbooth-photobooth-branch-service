namespace PhotoboothBranchService.Application.DTOs.PrintPricing
{
    public class UpdatePrintPricingRequest
    {
        public decimal DiscountPerPrintNumber { get; set; }
        public int MinQuantity { get; set; }
    }
}
