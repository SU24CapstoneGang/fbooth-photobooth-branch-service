using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Discount
    {
        public Guid DiscountID { get; set; }
        public string DiscountCode { get; set; }
        public int RemaniningUsage { get; set; }
        public Decimal DiscountRate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModified { get; set; }
        public DiscountStatus Status { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
