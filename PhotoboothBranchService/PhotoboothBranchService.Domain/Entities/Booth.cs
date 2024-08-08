﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Booth
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; } = default!;
        public decimal PricePerHour { get; set; }
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        public short PeopleInBooth { get; set; }
        public bool isBooked { get; set; }
        public BoothStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid BranchID { get; set; }
        public virtual Branch Branch { get; set; } = default!;
        public virtual ICollection<Booking> Bookings { get; set; } = default!;
        public virtual ICollection<Device> Devices { get; set; } = default!;
        public virtual ICollection<BoothPhoto> BoothPhotos { get; set; } = default!;
    }
}
