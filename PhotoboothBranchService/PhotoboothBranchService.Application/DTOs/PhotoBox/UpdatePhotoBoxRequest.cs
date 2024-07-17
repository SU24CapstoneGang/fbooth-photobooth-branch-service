namespace PhotoboothBranchService.Application.DTOs.PhotoBox
{
    public class UpdatePhotoBoxRequest
    {
        public int? boxHeight { get; set; }
        public int? boxWidth { get; set; }
        public int? CoordinatesX { get; set; }
        public int? CoordinatesY { get; set; }
        public bool? IsLandscape { get; set; }
        public int? BoxIndex { get; set; }
        public Guid? LayoutID { get; set; }
    }
}
