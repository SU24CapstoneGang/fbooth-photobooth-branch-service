using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Enum;

public enum ManufactureStatus
{
    Good = 1,
    Maintenance = 2,
    Discarded = 3,
}

public enum AccountStatus
{
    Active = 1,
    Blocked = 2,
}

public enum StatusUse
{
    Available = 1,
    Unusable = 2,
}
public enum PhotoPrivacy
{
    Public = 1,
    Private = 2
}

public enum PaymentStatus
{
    Active = 1,
    Inactive = 2,
}

public enum DiscountStatus
{
    Active = 1,
    Expired = 2,
}

