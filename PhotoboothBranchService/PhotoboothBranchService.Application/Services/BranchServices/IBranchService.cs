using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Application.DTOs.BranchPhoto;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BranchServices;

public interface IBranchService : IServiceBase<BranchResponse, BranchFilter, PagingModel>
{
    Task<IEnumerable<BranchResponse>> SearchByName(string name);
    Task<IEnumerable<BranchResponse>> GetByStatus(BranchStatus status);
    Task<CreateBranchResponse> CreateAsync(CreateBranchRequest createModel);
    Task<IEnumerable<BranchResponse>> GetAvailbleAsync();
    Task<BranchResponse> AddPhotoForBranch(Guid branchID, IFormFile file);

    Task UpdateAsync(Guid id, UpdateBranchRequest updateModel);
    Task DeleteAsync(Guid id);
}
