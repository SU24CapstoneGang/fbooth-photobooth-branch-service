using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoBoxServices
{
    public interface IPhotoBoxService : IServiceBase<PhotoBoxResponse, CreatePhotoBoxRequest, CreatePhotoBoxResponse, UpdatePhotoBoxRequest, PhotoBoxFilter, PagingModel>
    {
    }
}
