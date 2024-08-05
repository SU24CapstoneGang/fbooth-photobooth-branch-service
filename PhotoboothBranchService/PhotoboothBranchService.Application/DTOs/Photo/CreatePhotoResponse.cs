using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Photo
{
    public class CreatePhotoResponse
    {
        public Guid PhotoID { get; set; }
        public string PhotoURL { get; set; } = default!;
        public PhotoVersion Version { get; set; }
        public string CouldID { get; set; } = default!; //id from cloudinary service
        public DateTime CreateDate { get; set; }
        public Guid PhotoSessionID { get; set; }
        public Guid? BackgroundID { get; set; }
        public Dictionary<Guid, int> StickerList { get; set; } = new Dictionary<Guid, int>();
    }
}
