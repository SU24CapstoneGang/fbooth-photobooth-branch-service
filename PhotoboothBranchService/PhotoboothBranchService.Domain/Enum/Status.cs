namespace PhotoboothBranchService.Domain.Enum;

public enum ManufactureStatus
{
    Active = 1,
    Inactive = 2,
    Maintenance = 3,
    InUse = 4,
}

public enum AccountStatus
{
    Active = 1,
    Blocked = 0,
}

public enum StatusUse
{
    Available = 1,
    Unusable = 0,
}

public enum PaymentMethodStatus
{
    Active = 1,
    Inactive = 0,
}


public enum PaymentStatus
{
    Success = 1,
    Fail = 0,
    Processing = 2
}

public enum PhotoVersion
{
    Original = 0,
    Edited = 1,
}
