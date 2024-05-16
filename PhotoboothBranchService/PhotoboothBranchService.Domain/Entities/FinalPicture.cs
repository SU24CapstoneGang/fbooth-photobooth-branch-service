using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class FinalPicture
    {
        public Guid PictureID { get; set; }
        public string PictureURl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModified { get; set; }
        public PhotoPrivacy PicturePrivacy { get; set; }
        public int PrintQuantity { get; set; }
        public float PictureCost { get; set; }
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; }
        public virtual Order Order { get; set; }
        public Guid PrintPricingID { get; set; }
        public virtual PrintPricing PrintPricing { get; set; }
        public virtual EffectsPackLog EffectsPackLog { get; set; }
    }
}
