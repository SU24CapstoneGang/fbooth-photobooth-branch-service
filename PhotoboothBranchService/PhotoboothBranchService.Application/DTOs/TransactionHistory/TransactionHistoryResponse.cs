namespace PhotoboothBranchService.Application.DTOs.TransactionHistory
{
    public class TransactionHistoryResponse
    {
        public Guid TransactionID { get; set; }
        public int FinalPictureNumber { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? AccountID { get; set; }
    }
}
