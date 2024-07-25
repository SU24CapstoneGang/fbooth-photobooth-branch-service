namespace PhotoboothBranchService.Domain.Entities
{
    public class BookingService
    {
        public Guid BookingServiceID { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public Guid ServiceID { get; set; }
        public virtual ServicePackage Service { get; set; } = default!;
        public Guid SessionOrderID { get; set; }
        public virtual Booking SessionOrder { get; set; } = default!;
    }
}
