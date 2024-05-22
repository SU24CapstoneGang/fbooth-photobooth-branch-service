using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Discount
{
    public class Discountresponse
    {
        public Guid DiscountID { get; set; }
        public string DiscountCode { get; set; }
        public int RemaniningUsage { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModified { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
