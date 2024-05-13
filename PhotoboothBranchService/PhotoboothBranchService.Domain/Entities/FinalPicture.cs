using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class FinalPicture
    {
        public Guid PictureID { get; set; }
        public string PictureURl { get; set; }
        public DateTime CreateDate { get; set; }
        public PhotoPrivacy Privacy { get; set; }
        public Guid OrderID { get; set; }
        public virtual Order Order { get; set; }
        public Guid PrintPricingID { get; set; }
        public virtual PrintPricing PrintPricing { get; set; }
        public Guid PackID { get; set; }
        public virtual EffectsPack EffectsPack { get; set; }
    }
}
