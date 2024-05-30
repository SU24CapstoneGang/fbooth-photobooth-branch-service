namespace PhotoboothBranchService.Application.DTOs.Session
{
    public class CreateSessionRequest
    {
        public DateTime CreateDate { get; set; } = default!;
        public Guid BranchesID { get; set; }
    }
}
