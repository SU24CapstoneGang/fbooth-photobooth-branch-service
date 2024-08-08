using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.BranchPhoto
{
    public class BranchPhotoResponse
    {
        public Guid BranchPhotoId { get; set; }
        public string BranchPhotoUrl { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public Guid BranchID { get; set; }
    }
}
