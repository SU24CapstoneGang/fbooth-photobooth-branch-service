using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Photo;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.Photo
{
    public interface IPhotoService : IService<PhotoSessionResponse, CreatePhotoSessionRequest, UpdatePhotoSessionRequest, PhotoSessionFilter, PagingModel>
    {
    }
}
