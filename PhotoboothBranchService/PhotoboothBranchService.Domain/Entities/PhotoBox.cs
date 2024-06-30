namespace PhotoboothBranchService.Domain.Entities
{
    public class PhotoBox
    {
        public Guid PhotoBoxID { get; set; }
        public int boxHeight { get; set; }
        public int boxWidth { get; set; }
        public int CoordinatesX { get; set; }
        public int CoordinatesY { get; set; }
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; } = default!;
    }
}
