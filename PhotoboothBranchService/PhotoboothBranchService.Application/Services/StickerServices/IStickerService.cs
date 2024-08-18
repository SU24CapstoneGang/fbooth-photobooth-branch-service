using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Application.DTOs.Sticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.StickerServices;

public interface IStickerService : IServiceBase<StickerResponse, StickerFilter, PagingModel>
{
    Task<IEnumerable<StickerResponse>> GetByName(string name);
    Task DeleteAsync(Guid id);
    Task<StickerResponse> CreateStickerAsync(CreateStickerRequest request);
    Task UpdateStickerAsync(IFormFile file, Guid StickerId, UpdateStickerRequest updateStickerRequest);

}
