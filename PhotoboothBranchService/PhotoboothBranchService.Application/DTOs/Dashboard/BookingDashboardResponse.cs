using PhotoboothBranchService.Application.DTOs.Booking;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class BookingDashboardResponse
    {
        public List<BookingResponse> Canceleded { get; set; } = new List<BookingResponse>();
        public List<BookingResponse> Completed { get; set; } = new List<BookingResponse>();
        public List<BookingResponse> OnGoing { get; set; } = new List<BookingResponse>();
        public List<BookingResponse> InFuture { get; set; } = new List<BookingResponse>();
        public List<BookingResponse> NeedPayExtra { get; set; } = new List<BookingResponse>();
        public List<BookingResponse> NeedRefund { get; set; } = new List<BookingResponse>();
        public decimal TotalRevenue { get; set; } = 0;
        public decimal TotalRefunded { get; set; } = 0;
    }
}