namespace PhotoboothBranchService.Domain.Entities
{
    public class Order
    {
        public Guid OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int QuantityOfPicture { get; set; }
        public float TotalPrice { get; set; }
        public Guid SessionID { get; set; }
        public virtual Session Session { get; set; }
        public Guid PaymentID { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public Guid DiscountID { get; set; }
        public virtual Discount Discount { get; set; }
        public Guid PictureID { get; set; }
        public virtual FinalPicture FinalPicture { get; set; }
        public Guid? AccountID { get; set; }
        public virtual Account Account { get; set; }

    }
}
