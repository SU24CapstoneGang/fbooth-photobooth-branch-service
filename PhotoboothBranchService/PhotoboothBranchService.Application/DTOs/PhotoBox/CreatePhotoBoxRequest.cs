using System.Drawing;

namespace PhotoboothBranchService.Application.DTOs.PhotoBox
{
    public class CreatePhotoBoxRequest
    {
        public int BoxHeight { get; set; }
        public int BoxWidth { get; set; }
        public int CoordinatesX { get; set; }
        public int CoordinatesY { get; set; }
        public Guid LayoutID { get; set; }
    }
}
