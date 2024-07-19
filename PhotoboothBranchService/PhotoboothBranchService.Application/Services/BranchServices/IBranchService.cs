using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BoothBranchServices;

public interface IBranchService : IServiceBase<BranchResponse, BranchFilter, PagingModel>
{
    Task<IEnumerable<BranchResponse>> SearchByName(string name);
    Task<IEnumerable<BranchResponse>> GetByStatus(BranchStatus status);
    Task<CreateBranchResponse> CreateAsync(CreateBranchRequest createModel);
    Task UpdateAsync(Guid id, UpdateBranchRequest updateModel);
    Task DeleteAsync(Guid id);
}
