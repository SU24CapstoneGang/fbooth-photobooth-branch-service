using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BoothBranchServices;

public interface IBranchService : IService<BranchResponse,
    CreateBranchRequest,
    CreateBranchResponse,
    UpdateBranchRequest,
    BranchFilter,
    PagingModel>
{
    Task<IEnumerable<BranchResponse>> SearchByName(string name);
    Task<IEnumerable<BranchResponse>> GetByStatus(ManufactureStatus status);

}
