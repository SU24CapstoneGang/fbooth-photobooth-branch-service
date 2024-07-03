namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class ValidateSessionOrderRequest
    {
        public Guid BoothID { get; set; }
        public long ValidateCode { get; set; }
    }
}
