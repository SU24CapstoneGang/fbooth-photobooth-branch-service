using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.FinalPicture
{
    public class CreateFinalPictureRequest
    {
        public string PictureURl { get; set; }
        //public string PublicId { get; set; }
        public PhotoPrivacy PicturePrivacy { get; set; }
        public int PrintQuantity { get; set; }
        public float PictureCost { get; set; }
        public Guid LayoutID { get; set; }
        public Guid PrintPricingID { get; set; }
    }
}
