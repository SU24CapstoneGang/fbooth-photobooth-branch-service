using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class BookingSlot
    {
        public Guid BookingSlotID { get; set; }
        public DateOnly BookingDate { get; set; }
        public Guid BookingID { get; set; }
        public virtual Booking Booking { get; set; }
        public Guid SlotID { get; set; }
        public virtual Slot Slot { get; set; }
    }
}
