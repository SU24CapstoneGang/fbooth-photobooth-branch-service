namespace PhotoboothBranchService.Application.DTOs.PhotoSticker
{
    public class CreatePhotoStickerResponse
    {
        public Guid PhotoStickerID { get; set; }
        public short Quantity { get; set; }
        public Guid? StickerID { get; set; }
        public Guid PhotoID { get; set; }
    }
}
