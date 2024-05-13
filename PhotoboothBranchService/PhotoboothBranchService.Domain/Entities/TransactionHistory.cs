namespace PhotoboothBranchService.Domain.Entities
{
    public class TransactionHistory
    {
        public Guid TransactionID { get; set; }
        public int FinalPictureNumber { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
