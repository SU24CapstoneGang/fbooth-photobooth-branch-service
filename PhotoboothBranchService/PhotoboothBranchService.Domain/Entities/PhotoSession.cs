﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class PhotoSession
    {
        public Guid PhotoSessionID { get; set; }
        public int SessionIndex { get; set; }
        public int TotalPhotoTaken { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid LayoutID { get; set; }
        public PhotoSessionStatus Status { get; set; }
        public virtual Layout Layout { get; set; } = default!;
        public Guid BookingID { get; set; }
        public virtual Booking Booking { get; set; } = default!;
        public virtual ICollection<Photo> Photos { get; set; } = default!;
    }
}
