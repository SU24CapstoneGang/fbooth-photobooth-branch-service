using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoStickerServices
{
    public interface IPhotoStickerService : IService<PhotoStickerResponse, CreatePhotoStickerRequest, CreatePhotoStickerResponse, UpdatePhotoStickerRequest, PhotoStickerFilter, PagingModel>
    {
    }
}
