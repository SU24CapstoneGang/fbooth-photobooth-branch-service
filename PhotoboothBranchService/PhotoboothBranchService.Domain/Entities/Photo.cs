using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Photo
    {
        public Guid PhotoID { get; set; }
        public string PhotoURL { get; set; } = default!;
        public PhotoVersion Version { get; set; }
        public string CouldID { get; set; } = default!; //id from cloudinary service
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid PhotoSessionID { get; set; }
        public virtual PhotoSession PhotoSession { get; set; } = default!;
        public Guid? BackgroundID { get; set; }
        public virtual Background Background { get; set; } = default!;
        public virtual ICollection<PhotoSticker> PhotoStickers { get; set; } = default!;
    }
}
