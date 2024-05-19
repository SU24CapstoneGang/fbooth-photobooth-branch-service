using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Role;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Role;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.RoleServices;

public interface IRoleService : IService<RoleResponse,CreateRoleRequest,UpdateRoleRequest,RoleFilter,PagingModel>
{
    Task<IEnumerable<RoleResponse>> GetByName(string name);
}
