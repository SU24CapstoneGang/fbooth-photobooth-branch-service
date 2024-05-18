using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities;

public class Account 
{
    public Guid AccountID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public AccountStatus Status { get; set; }
    public Guid RoleID { get; set; }
    public virtual Role Role { get; set; }
    public virtual List<PhotoBoothBranch> PhotoBoothBranches { get; set; }
    public virtual List<TransactionHistory> TransactionHistories { get; set; }
    public virtual List<Order> Orders { get; set; }

    public void SetPassword(string plainTextPassword, IPasswordHasher passwordHasher)
    {
        PasswordSalt = passwordHasher.GenerateSalt();
        PasswordHash = passwordHasher.HashPassword(plainTextPassword, PasswordSalt);
    }

    public bool VerifyPassword(string plainTextPassword, IPasswordHasher passwordHasher)
    {
        var hash = passwordHasher.HashPassword(plainTextPassword, PasswordSalt);
        return hash.SequenceEqual(PasswordHash);
    }
}
