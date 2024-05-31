namespace PhotoboothBranchService.Application.DTOs.TransactionHistory
{
    public class CreateTransactionHistoryRequest
    {
        public int FinalPictureNumber { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? AccountID { get; set; }
    }
}
