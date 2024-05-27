using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Account
{
    public class AccountRegisterResponse
    {
        public Guid AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public AccountStatus Status { get; set; }
        public string RoleName { get; set; }
    }
}
