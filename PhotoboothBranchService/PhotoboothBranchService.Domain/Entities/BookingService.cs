namespace PhotoboothBranchService.Domain.Entities
{
    public class BookingService
    {
        public Guid BookingServiceID { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public Guid ServiceID { get; set; }
        public virtual Service Service { get; set; }
        public Guid BookingID { get; set; }
        public virtual Booking Booking { get; set; } = default!;
    }
}
