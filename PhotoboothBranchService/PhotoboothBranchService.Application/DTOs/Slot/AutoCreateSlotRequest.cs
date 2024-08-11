using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Slot
{
    public class AutoCreateSlotRequest
    {
        public Guid BoothID { get; set; }
        public decimal Price { get; set; }
    }
}
