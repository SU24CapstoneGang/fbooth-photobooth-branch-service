namespace PhotoboothBranchService.Domain.Entities
{
    public class TransactionHistory
    {
        public Guid TransactionID { get; set; }
        public int FinalPictureNumber { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? AccountID { get; set; }
        public virtual Account Account { get; set; }

    }
}
