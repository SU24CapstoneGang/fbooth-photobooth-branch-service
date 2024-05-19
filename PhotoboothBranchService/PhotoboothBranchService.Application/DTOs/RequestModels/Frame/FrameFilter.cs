namespace PhotoboothBranchService.Application.DTOs.RequestModels.Frame
{
    public class FrameFilter
    {
        public string? FrameName { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public DateTime? LastModifiedFrom { get; set; }
        public DateTime? LastModifiedTo { get; set; }
    }
}
