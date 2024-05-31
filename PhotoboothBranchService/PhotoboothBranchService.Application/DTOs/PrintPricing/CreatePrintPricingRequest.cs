namespace PhotoboothBranchService.Application.DTOs.PrintPricing
{
    public class CreatePrintPricingRequest
    {
        public decimal DiscountPerPrintNumber { get; set; }
        public int MinQuantity { get; set; }
    }
}
