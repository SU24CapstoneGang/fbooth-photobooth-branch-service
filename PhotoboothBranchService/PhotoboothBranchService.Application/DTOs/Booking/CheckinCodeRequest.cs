namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class CheckinCodeRequest
    {
        public Guid BoothID { get; set; }
        public long Code { get; set; }
    }
}
