using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionPackage;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.SessionPackageServices
{
    public interface ISessionPackageService : IServiceBase<SessionPackageResponse, CreateSessionPackageRequest, CreateSessionPackageResponse, UpdateSessionPackageRequest, SessionPackageFilter, PagingModel>
    {
    }
}
