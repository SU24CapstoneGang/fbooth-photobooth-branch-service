using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Sticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.StickerServices;

public interface IStickerService : IServiceBase<StickerResponse, StickerFilter, PagingModel>
{
    Task<IEnumerable<StickerResponse>> GetByName(string name);
    Task<StickerResponse> CreateStickerAsync(IFormFile file, CreateStickerRequest createModel);
    public Task<CreateStickerResponse> CreateAsync(CreateStickerRequest createModel);
    public Task UpdateAsync(Guid id, UpdateStickerRequest updateModel);
    public Task DeleteAsync(Guid id);
}
