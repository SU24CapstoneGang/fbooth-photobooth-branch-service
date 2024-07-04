namespace PhotoboothBranchService.Application.DTOs.PhotoBox
{
    public class PhotoBoxFilter
    {
        public int? boxHeight { get; set; }
        public int? boxWidth { get; set; }
        public int? CoordinatesX { get; set; }
        public int? CoordinatesY { get; set; }
        public Guid? LayoutID { get; set; }
    }
}
