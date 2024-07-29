namespace PhotoboothBranchService.Domain.Enum
{
    public enum BookingStatus
    {
        Processsing = 1, // session is running
        Done = 3, // the session end in a normal way
        Canceled = 2, //for some reason, this session got end without paying
        Waiting = 0, //waiting for active by code
        Deposited = 4, //
        Created = 5 //session just created


        //    Processsing = 0, // session is running
        //Complete = 1, // the session end in a normal way
        //Waiting = 2, //waiting for active by code
        //CodeChecked = 3 // code has been checked, and customer is taking photo
    }
}
