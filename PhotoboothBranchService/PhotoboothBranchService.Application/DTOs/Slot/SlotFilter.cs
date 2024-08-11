using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Slot
{
    public class SlotFilter
    {
        public TimeSpan SlotStartTime { get; set; }
        public TimeSpan SlotEndTime { get; set; }
        public decimal Price { get; set; }
        public StatusUse Status { get; set; }
        public Guid BoothID { get; set; }
    }
}
