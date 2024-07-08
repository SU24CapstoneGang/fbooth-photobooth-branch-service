using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoSessionServices
{
    public interface IPhotoSessionService : IServiceBase<PhotoSessionResponse, PhotoSessionFilter, PagingModel>
    {
        public Task<CreatePhotoSessionResponse> CreateAsync(CreatePhotoSessionRequest createModel);
        public Task UpdateAsync(Guid id, UpdatePhotoSessionRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
