using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class AdminBoothResponse
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; } = default!;
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        public short PeopleInBooth { get; set; }
        public string ActiveCode { get; set; } = default!;
        public BoothStatus Status { get; set; }
        public Guid BranchID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
