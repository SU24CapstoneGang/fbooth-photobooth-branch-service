using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PhotoSessionServices
{
    public interface IPhotoSessionService : IServiceBase<PhotoSessionResponse, PhotoSessionFilter, PagingModel>
    {
        Task<CreatePhotoSessionResponse> CreateAsync(CreatePhotoSessionRequest createModel);
        Task UpdateAsync(Guid id, UpdatePhotoSessionRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
