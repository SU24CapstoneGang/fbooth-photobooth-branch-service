namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class ValidateSessionOrderRequest
    {
        public Guid BoothID { get; set; }
        public long ValidateCode { get; set; }
    }
}
