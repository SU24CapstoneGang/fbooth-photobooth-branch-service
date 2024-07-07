using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BoothBranchServices;

public interface IBranchService : IServiceBase<BranchResponse, BranchFilter, PagingModel>
{
    Task<IEnumerable<BranchResponse>> SearchByName(string name);
    Task<IEnumerable<BranchResponse>> GetByStatus(ManufactureStatus status);
    public Task<CreateBranchResponse> CreateAsync(CreateBranchRequest createModel);
    public Task UpdateAsync(Guid id, UpdateBranchRequest updateModel);
    public Task DeleteAsync(Guid id);
}
