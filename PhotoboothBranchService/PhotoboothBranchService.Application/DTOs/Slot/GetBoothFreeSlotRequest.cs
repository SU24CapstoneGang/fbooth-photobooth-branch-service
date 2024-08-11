using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Slot
{
    public class GetBoothFreeSlotRequest
    {
        public Guid BoothID { get; set; }
        public DateOnly date {  get; set; }
        public TimeSpan? startTime { get; set; }
        public TimeSpan? endTime { get; set; }
    }
}
