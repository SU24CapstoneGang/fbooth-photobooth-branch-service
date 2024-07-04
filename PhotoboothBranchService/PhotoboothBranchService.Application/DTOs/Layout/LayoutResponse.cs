using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Layout
{
    public class LayoutResponse
    {
        public Guid LayoutID { get; set; }
        public string LayoutURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public string LayoutCode { get; set; }
        public StatusUse Status { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public short PhotoSlot { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public List<PhotoBoxResponse> PhotoBoxes { get; set; }
        public List<BackgroundResponse> Backgrounds { get; set; } = default!;

    }
}
