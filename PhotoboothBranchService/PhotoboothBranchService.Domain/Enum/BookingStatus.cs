using System.Runtime.Serialization;

namespace PhotoboothBranchService.Domain.Enum
{
    public enum BookingStatus
    {
        [EnumMember(Value = "Pending Checking")]
        PendingChecking = 2,
        [EnumMember(Value = "No Show")]
        NoShow = 3, //customer not come
        [EnumMember(Value = "Pending Payment")]
        PendingPayment = 1, //waiting pay
        [EnumMember(Value = "Completed Checking")]
        CompleteChecked = 5,
        [EnumMember(Value = "Taking Photo")]
        TakingPhoto = 4,
        Canceled = 6,
        [EnumMember(Value = "Extra Service")]
        ExtraService = 7,
        [EnumMember(Value = "Cancelled By System")]
        CancelledBySystem = 8
    }
}
