namespace PhotoboothBranchService.Application.DTOs.RequestModels.Sticker
{
    public class UpdateStickerRequest
    {
        public string StickerName { get; set; } = default!;
        public string StickerURL { get; set; } = default!;
    }
}
