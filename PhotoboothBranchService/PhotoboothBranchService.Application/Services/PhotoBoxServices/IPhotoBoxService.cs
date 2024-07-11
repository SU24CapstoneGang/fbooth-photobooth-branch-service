using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoBoxServices
{
    public interface IPhotoBoxService : IServiceBase<PhotoBoxResponse, PhotoBoxFilter, PagingModel>
    {
        Task<CreatePhotoBoxResponse> CreateAsync(CreatePhotoBoxRequest createModel);
        Task UpdateAsync(Guid id, UpdatePhotoBoxRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
