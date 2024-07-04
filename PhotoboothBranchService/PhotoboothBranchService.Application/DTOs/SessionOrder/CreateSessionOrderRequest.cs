namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class CreateSessionOrderRequest
    {
        public Guid BoothID { get; set; }
        public Guid AccountID { get; set; }
        public Guid SessionPackageID { get; set; }
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
        public DateTime? StartTime { get; set; }
    }
}
