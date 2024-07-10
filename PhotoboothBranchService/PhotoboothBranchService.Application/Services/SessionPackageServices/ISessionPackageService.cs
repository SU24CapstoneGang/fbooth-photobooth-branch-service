using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionPackage;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.SessionPackageServices
{
    public interface ISessionPackageService : IServiceBase<SessionPackageResponse, SessionPackageFilter, PagingModel>
    {
        Task<CreateSessionPackageResponse> CreateAsync(CreateSessionPackageRequest createModel);
        Task UpdateAsync(Guid id, UpdateSessionPackageRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
