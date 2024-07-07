using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoBoxServices
{
    public interface IPhotoBoxService : IServiceBase<PhotoBoxResponse, PhotoBoxFilter, PagingModel>
    {
        public Task<CreatePhotoBoxResponse> CreateAsync(CreatePhotoBoxRequest createModel);
        public Task UpdateAsync(Guid id, UpdatePhotoBoxRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
