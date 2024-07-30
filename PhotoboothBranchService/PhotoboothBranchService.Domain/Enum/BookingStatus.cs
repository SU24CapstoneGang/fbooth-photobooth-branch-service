namespace PhotoboothBranchService.Domain.Enum
{
    public enum BookingStatus
    {
        Completed = 3, 
        Canceled = 2, 
        NoShow = 1, //customer not come
        PendingPayment = 4, //waiting pay
        Refunded = 5 
    }
}
