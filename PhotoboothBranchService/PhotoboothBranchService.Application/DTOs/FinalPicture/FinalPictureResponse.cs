using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.FinalPicture
{
    public class FinalPictureResponse
    {
        public Guid PictureID { get; set; }
        public string PictureURl { get; set; }
        public DateTime CreateDate { get; set; }
        public PhotoPrivacy PicturePrivacy { get; set; }
    }
}
