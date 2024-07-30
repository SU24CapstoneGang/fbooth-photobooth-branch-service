namespace PhotoboothBranchService.Domain.Enum;

public enum BoothStatus
{
    Active = 1,
    Inactive = 2,
    Maintenance = 3,
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

public enum PhotoVersion
{
    Original = 0,
    Edited = 1,
}

