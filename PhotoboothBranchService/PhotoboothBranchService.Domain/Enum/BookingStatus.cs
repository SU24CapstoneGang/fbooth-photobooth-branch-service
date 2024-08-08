namespace PhotoboothBranchService.Domain.Enum
{
    public enum BookingStatus
    {
        PendingChecking = 2,
        NoShow = 3, //customer not come
        PendingPayment = 1, //waiting pay
        CompleteChecked = 5,
        TakingPhoto = 4,
        Canceled = 6,
        ExtraService = 7
    }
}
