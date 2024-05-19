using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ResponseModels.PhotoBoothBranch
{
    public class PhotoBoothBranchresponse
    {
        public Guid BranchesID { get; set; } = default!;
        public string BranchName { get; set; } = default!;
        public string BranchAddress { get; set; } = default!;
        public ManufactureStatus Status { get; set; } = default!;
        public Guid AccountID { get; set; }
        public string AccountName { get; set; }
        public string CameraModelName { get; set; }
        public string PrinterModelName { get; set; }

    }
}
