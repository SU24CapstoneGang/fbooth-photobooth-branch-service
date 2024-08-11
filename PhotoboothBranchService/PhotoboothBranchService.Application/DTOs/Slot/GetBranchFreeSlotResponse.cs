using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Slot
{
    public class GetBranchFreeSlotResponse
    {
        public Guid BoothID { get; set; }
        public IEnumerable<SlotResponse> Slots { get; set; } = Enumerable.Empty<SlotResponse>();
    }
}
