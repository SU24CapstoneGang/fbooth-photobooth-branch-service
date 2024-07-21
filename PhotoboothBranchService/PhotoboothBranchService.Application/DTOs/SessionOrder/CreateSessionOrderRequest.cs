using PhotoboothBranchService.Application.Common.Helpers;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class CreateSessionOrderRequest
    {
        public Guid BoothID { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public Guid SessionPackageID { get; set; }
        [DictionaryValueGreaterThanZero(ErrorMessage = "Each service quantity must be greater than 0.")]
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
        public DateTime? StartTime { get; set; }
    }
}
