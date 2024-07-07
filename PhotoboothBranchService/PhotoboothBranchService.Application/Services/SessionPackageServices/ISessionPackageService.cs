using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionPackage;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.SessionPackageServices
{
    public interface ISessionPackageService : IServiceBase<SessionPackageResponse, SessionPackageFilter, PagingModel>
    {
        public Task<CreateSessionPackageResponse> CreateAsync(CreateSessionPackageRequest createModel);
        public Task UpdateAsync(Guid id, UpdateSessionPackageRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
