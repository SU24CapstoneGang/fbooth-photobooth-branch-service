using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Account
{
    public class UpdateAccountRequestModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 100 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; }

        [RegularExpression(@"^0[1-9]\d{8}$", ErrorMessage = "Invalid PhoneNumber format")]
        public string PhoneNumber { get; set; }
    }
}
