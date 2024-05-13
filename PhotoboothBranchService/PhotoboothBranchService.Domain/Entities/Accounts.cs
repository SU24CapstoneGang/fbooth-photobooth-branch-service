using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class Accounts : BaseEntity
{
    public string EmailAddress { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid? PhotoBoothBranchId { get; set; }
    public AccountRole Role { get; set; }
    public AccountStatus Status { get; set; }
    public virtual PhotoBoothBranches? PhotoBoothBranch { get; set; }

    private Accounts()
    {
    }
}
