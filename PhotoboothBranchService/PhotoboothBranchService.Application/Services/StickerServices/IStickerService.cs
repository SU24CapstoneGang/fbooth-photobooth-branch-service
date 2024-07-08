using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Application.DTOs.Sticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.StickerServices;

public interface IStickerService : IServiceBase<StickerResponse, StickerFilter, PagingModel>
{
    public Task<IEnumerable<StickerResponse>> GetByName(string name);
    public Task DeleteAsync(Guid id);
    public Task<StickerResponse> CreateStickerAsync(IFormFile file);
    public Task UpdateStickerAsync(IFormFile file, Guid StickerId, UpdateStickerRequest updateStickerRequest);

}
