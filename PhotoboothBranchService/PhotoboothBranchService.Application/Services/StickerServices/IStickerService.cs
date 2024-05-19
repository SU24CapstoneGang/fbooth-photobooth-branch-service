using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Sticker;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Sticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.StickerServices;

public interface IStickerService : IService<StickerResponse,CreateStickerRequest,UpdateStickerRequest,StickerFilter,PagingModel>
{
    Task<IEnumerable<StickerResponse>> GetByName(string name);
}
