using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class FinalPicture
    {
        public Guid PictureID { get; set; }
        public string PictureURl { get; set; } = default!;
        public string PublicId { get; set; }
        public DateTime CreateDate { get; set; }
        public PhotoPrivacy PicturePrivacy { get; set; }
        public Guid SessionID { get; set; }
        public virtual Session Session { get; set; }
        public virtual EffectsPackLog EffectsPackLog { get; set; }
    }
}
