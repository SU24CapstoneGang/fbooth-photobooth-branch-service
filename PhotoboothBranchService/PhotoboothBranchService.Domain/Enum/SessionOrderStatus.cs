namespace PhotoboothBranchService.Domain.Enum
{
    public enum SessionOrderStatus
    {
        Processsing = 1, // session is running
        Paid = 3, // the session end in a normal way
        Canceled = 2, //for some reason, this session got end without paying
        Waiting = 0, //waiting for active by code
    }
}
