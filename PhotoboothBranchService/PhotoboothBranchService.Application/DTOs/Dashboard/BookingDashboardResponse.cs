namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class BookingDashboardResponse
    {
        public int Canceleded { get; set; } = 0;
        public int Completed { get; set; } = 0;
        public int OnGoing { get; set; } = 0;
        public int InFuture { get; set; } = 0;
        public int NeedPayExtra { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;
        public decimal TotalRefunded { get; set; } = 0;
    }
}