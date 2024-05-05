using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Enum;

public enum ManufactureStatus
{
    good = 1,
    maintenance = 2,
    discarded = 3,
}

public enum AccountStatus
{
    active = 1,
    blocked = 2,
}

public enum AccountRole
{
    admin = 1,
    manager = 2,
}
