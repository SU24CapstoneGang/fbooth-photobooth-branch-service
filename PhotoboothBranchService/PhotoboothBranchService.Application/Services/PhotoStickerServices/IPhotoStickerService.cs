using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoStickerServices
{
    public interface IPhotoStickerService : IServiceBase<PhotoStickerResponse, PhotoStickerFilter, PagingModel>
    {
        public Task<CreatePhotoStickerResponse> CreateAsync(CreatePhotoStickerRequest createModel);
        public Task UpdateAsync(Guid id, UpdatePhotoStickerRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
