using PhotoboothBranchService.Application.DTOs.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.BookingSlot
{
    public class BookingSlotResponse
    {
        public Guid BookingSlotID { get; set; }
        public DateOnly BookingDate { get; set; }
        public decimal Price { get; set; }
        public TimeSpan SlotStartTime { get; set; }
        public TimeSpan SlotEndTime { get; set; }
    }
}
