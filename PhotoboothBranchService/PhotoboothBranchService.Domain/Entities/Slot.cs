using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Slot
    {
        public Guid SlotID { get; set; }
        public TimeSpan SlotStartTime { get; set; }
        public TimeSpan SlotEndTime { get; set; }
        public decimal Price { get; set; }
        public StatusUse Status {  get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid BoothID { get; set; }
        public virtual Booth Booth { get; set; } = default!;
        public virtual ICollection<BookingSlot> BookingSlots { get; set; } = default!;
    }
}
