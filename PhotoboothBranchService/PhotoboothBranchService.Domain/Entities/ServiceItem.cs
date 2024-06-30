namespace PhotoboothBranchService.Domain.Entities
{
    public class ServiceItem
    {
        public Guid ServiceItemID { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public Guid ServiceID { get; set; }
        public virtual Service Service { get; set; } = default!;
        public Guid SessionOrderID { get; set; }
        public virtual SessionOrder SessionOrder { get; set; } = default!;
    }
}
