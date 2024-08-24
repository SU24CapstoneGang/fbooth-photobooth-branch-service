using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Booth
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; } = default!;
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        public short PeopleInBooth { get; set; }
        public BoothStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid BranchID { get; set; }
        public virtual Branch Branch { get; set; } = default!;
        public virtual ICollection<Booking> Bookings { get; set; } = default!;
        public virtual ICollection<Slot> Slots { get; set; } = default!;
        public virtual ICollection<BoothPhoto> BoothPhotos { get; set; } = default!;
    }
}
