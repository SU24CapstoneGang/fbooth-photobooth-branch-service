using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Authentication
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
