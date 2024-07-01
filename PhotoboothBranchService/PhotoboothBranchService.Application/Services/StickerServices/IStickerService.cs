using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Sticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.StickerServices;

public interface IStickerService : IService<StickerResponse, CreateStickerRequest, CreateStickerResponse, UpdateStickerRequest, StickerFilter, PagingModel>
{
    Task<IEnumerable<StickerResponse>> GetByName(string name);
    Task<StickerResponse> CreateStickerAsync(IFormFile file, CreateStickerRequest createModel);
}
