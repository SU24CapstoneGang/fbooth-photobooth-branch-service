namespace PhotoboothBranchService.Domain.Entities
{
    public class PhotoBox
    {
        public Guid PhotoBoxID { get; set; }
        public int BoxHeight { get; set; }
        public int BoxWidth { get; set; }
        public int CoordinatesX { get; set; }
        public int CoordinatesY { get; set; }
        public bool IsLandscape { get; set; }
        public int BoxIndex { get; set; }
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; } = default!;
    }
}
