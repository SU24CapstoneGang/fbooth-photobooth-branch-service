namespace PhotoboothBranchService.Domain.Entities
{
    public class Order
    {
        public Guid OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int PhotoQuantity { get; set; }
        public float TotalPrice { get; set; }
        public Guid SessionID { get; set; }
        public virtual Session Session { get; set; }
        public Guid PaymentID { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public Guid DiscountID { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual List<FinalPicture> FinalPictures { get; set; }

    }
}
