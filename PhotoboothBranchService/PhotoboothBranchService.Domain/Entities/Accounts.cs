using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class Accounts : BaseEntity
{
    public string EmailAddress { get; } = null!;
    public string PhoneNumber { get; } = null!;
    public string Password { get; } = null!;
    public Guid? PhotoBoothBrachId { get; }
    public AccountRole Role { get; }
    public AccountStatus Status { get; }
    public virtual PhotoBoothBranches? PhotoBoothBranch { get; }

    //no password
    public Accounts(Guid id, string emailAddress, string phoneNumber, AccountRole role, AccountStatus status, Guid? photoBoothBrachId) : base(id)
    {
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Role = role;
        Status = status;
        PhotoBoothBrachId = photoBoothBrachId;
    }

    public Accounts(Guid id, string emailAddress, string phoneNumber, string password, AccountRole role, AccountStatus status, Guid? photoBoothBrachId) : base(id)
    {
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Password = password;
        Role = role;
        Status = status;
        PhotoBoothBrachId = photoBoothBrachId;
    }

    public Accounts(Guid id, string emailAddress, string phoneNumber, string password, AccountRole role, AccountStatus status) : base(id)
    {
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Password = password;
        Role = role;
        Status = status;
    }

    private Accounts()
    {
    }
}
