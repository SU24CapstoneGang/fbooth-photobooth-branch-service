using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Account
{
    public class UpdateAccountRequestModel
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 100 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid DateOfBirth format")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [RegularExpression(@"^(0[1-9])+([0-9]{8})$", ErrorMessage = "Invalid PhoneNumber format")]
        public string PhoneNumber { get; set; }
    }
}
