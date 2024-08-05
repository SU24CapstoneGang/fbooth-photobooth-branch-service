using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Photo
{
    public class CreatePhotoRequest
    {
        public PhotoVersion Version { get; set; }
        public Guid PhotoSessionID { get; set; }
        public Guid? BackgroundID { get; set; }
        [DictionaryValueGreaterThanZero]
        public Dictionary<Guid, int> StickerList { get; set; } = new Dictionary<Guid, int>();
    }
}
