using PhotoboothBranchService.Application.DTOs.Camera;
using PhotoboothBranchService.Application.DTOs.Printer;
using PhotoboothBranchService.Application.DTOs.Session;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PhotoBoothBranch
{
    public class PhotoBoothBranchresponse
    {
        public Guid PhotoBoothBranchID { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public DateTime CreateDate { get; set; }
        public ManufactureStatus Status { get; set; }
        public Guid? AccountID { get; set; }
        public string? CameraName { get; set; }
        public string? PrinterName { get; set; }
    }
}
