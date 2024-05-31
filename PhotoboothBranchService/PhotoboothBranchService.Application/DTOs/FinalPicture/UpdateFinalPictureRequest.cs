using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.FinalPicture
{
    public class UpdateFinalPictureRequest
    {
        public string PictureURl { get; set; }
        public PhotoPrivacy PicturePrivacy { get; set; }
        public int PrintQuantity { get; set; }
        public float PictureCost { get; set; }
        public Guid LayoutID { get; set; }
        public Guid PrintPricingID { get; set; }
    }
}
