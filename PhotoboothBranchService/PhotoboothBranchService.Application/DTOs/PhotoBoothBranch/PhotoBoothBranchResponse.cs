using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PhotoBoothBranch
{
    public class PhotoBoothBranchresponse
    {
        public Guid BranchesID { get; set; } = default!;
        public string BranchName { get; set; } = default!;
        public string BranchAddress { get; set; } = default!;
        public ManufactureStatus Status { get; set; } = default!;
        public Guid AccountID { get; set; }
        //public string AccountName { get; set; }
        //public string CameraModelName { get; set; }
        //public string PrinterModelName { get; set; }

    }
}
