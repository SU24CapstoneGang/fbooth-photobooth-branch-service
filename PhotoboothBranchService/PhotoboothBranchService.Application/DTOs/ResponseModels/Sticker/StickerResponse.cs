namespace PhotoboothBranchService.Application.DTOs.ResponseModels.Sticker
{
    public class StickerResponse
    {
        public Guid StickerId { get; set; }
        public string StickerName { get; set; } = default!;
        public string StickerURL { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
