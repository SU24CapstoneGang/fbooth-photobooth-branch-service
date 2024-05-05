using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class Accounts : Entity
{
    public String EmailAddress { get; } = null;
    public String PhoneNumber { get; } = null;
    public AccountRole Role { get; }
    public AccountStatus Status { get; }

   public Accounts(Guid id, String emailAddress, AccountRole role, AccountStatus status)
    {
        EmailAddress = emailAddress;
        Role = role;
        Status = status;
    }
}
