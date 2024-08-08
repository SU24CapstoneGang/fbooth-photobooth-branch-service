using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Account
{
    public class UpdateAccountRequestModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; }

        [RegularExpression(@"^0[1-9]\d{8}$", ErrorMessage = "Invalid PhoneNumber format")]
        public string PhoneNumber { get; set; }
    }
}
