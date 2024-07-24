using PhotoboothBranchService.Application.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class CreateSessionOrderRequest
    {
        public Guid BoothID { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string? CustomerEmail { get; set; }
        [RegularExpression(@"^0[1-9]\d{8}$", ErrorMessage = "Invalid PhoneNumber format")]
        public string? CustomerPhoneNumber { get; set; }
        [DictionaryValueGreaterThanZero(ErrorMessage = "Each service quantity must be greater than 0.")]
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; } 
    }
}
