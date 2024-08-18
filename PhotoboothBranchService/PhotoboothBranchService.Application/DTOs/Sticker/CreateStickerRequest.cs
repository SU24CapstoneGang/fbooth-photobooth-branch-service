using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Sticker
{
    public class CreateStickerRequest
    {
        public IFormFile File {  get; set; }
        public Guid StickerTypeID { get; set; }
    }
}
