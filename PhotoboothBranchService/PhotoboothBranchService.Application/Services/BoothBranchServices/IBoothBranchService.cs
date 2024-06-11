using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BoothBranchServices;

public interface IBoothBranchService : IService<BoothBranchResponse,
    CreateBoothBranchRequest,
    UpdateBoothBranchRequest,
    BoothBranchFilter,
    PagingModel>
{
    Task<IEnumerable<BoothBranchResponse>> SearchByName(string name);
    Task<IEnumerable<BoothBranchResponse>> GetByStatus(ManufactureStatus status);

}
