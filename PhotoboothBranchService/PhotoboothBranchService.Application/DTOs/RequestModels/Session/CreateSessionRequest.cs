namespace PhotoboothBranchService.Application.DTOs.RequestModels.Session
{
    public class CreateSessionRequest
    {
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
        public Guid BranchesID { get; set; }
    }
}
