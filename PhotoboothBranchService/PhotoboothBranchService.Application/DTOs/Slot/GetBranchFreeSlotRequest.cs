using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Slot
{
    public class GetBranchFreeSlotRequest
    {
        public Guid BranchID { get; set; }
        public DateOnly date { get; set; }
    }
}
