namespace PhotoboothBranchService.Application.DTOs.TransactionHistory
{
    public class UpdateTransactionHistoryRequest
    {
        public int FinalPictureNumber { get; set; }
        public string Description { get; set; }
        public Guid? AccountID { get; set; }
    }
}
