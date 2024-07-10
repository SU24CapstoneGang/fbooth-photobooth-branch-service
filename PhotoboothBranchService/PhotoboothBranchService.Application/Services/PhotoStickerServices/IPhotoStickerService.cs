using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoStickerServices
{
    public interface IPhotoStickerService : IServiceBase<PhotoStickerResponse, PhotoStickerFilter, PagingModel>
    {
        Task<CreatePhotoStickerResponse> CreateAsync(CreatePhotoStickerRequest createModel);
        Task UpdateAsync(Guid id, UpdatePhotoStickerRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
