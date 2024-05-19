namespace PhotoboothBranchService.Application.DTOs.RequestModels.Sticker
{
    public class CreateStickerRequest
    {
        public string StickerName { get; set; } = default!;
        public string StickerURL { get; set; } = default!;
    }
}
