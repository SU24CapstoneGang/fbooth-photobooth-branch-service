using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BoothBranchServices;

public interface IBranchService : IServiceBase<BranchResponse, BranchFilter, PagingModel>
{
    Task<IEnumerable<BranchResponse>> SearchByName(string name);
    Task<IEnumerable<BranchResponse>> GetByStatus(BranchStatus status);
    Task<CreateBranchResponse> CreateAsync(CreateBranchRequest createModel, BranchStatus status);
    Task UpdateAsync(Guid id, UpdateBranchRequest updateModel, BranchStatus? status);
    Task AssignManager(Guid branchId, AssignManagerRequest request);
    Task DeleteAsync(Guid id);
}
