namespace PhotoboothBranchService.Application.DTOs.BookingService
{
    public class BookingServiceFilter
    {
        public short? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? Measure { get; set; }
        public string? Unit { get; set; } = default!;
        public Guid? ServiceID { get; set; }
        public Guid? BookingID { get; set; }
    }
}
