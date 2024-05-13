using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerID { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public AccountStatus Status { get; set; } = default!;
        public virtual List<Session> Sessions { get; set; }
        public virtual List<TransactionHistory> TransactionHistories { get; set; }
    }
}
