using AutoMapper;
using Beanbox.Business.Commons.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Role;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Role;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.RoleServices;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreateRoleRequest createModel)
    {
        Role role = _mapper.Map<Role>(createModel);
        role.RoleID = Guid.NewGuid();
        await _roleRepository.AddAsync(role);
        return role.RoleID;
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var role = (await _roleRepository.GetAsync(r => r.RoleID == id)).FirstOrDefault();
            if (role != null)
            {
                await _roleRepository.RemoveAsync(role);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<RoleResponse>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RoleResponse>>(roles.ToList());
    }

    public async Task<IEnumerable<RoleResponse>> GetAllPagingAsync(RoleFilter filter, PagingModel paging)
    {
        var roles = (await _roleRepository.GetAllAsync()).AutoPaging(paging.PageSize,paging.PageIndex);
        var listRoleresponse = _mapper.Map<IEnumerable<RoleResponse>>(roles.ToList());
        listRoleresponse.AutoFilter(filter);
        return listRoleresponse;
    }

    public async Task<RoleResponse> GetByIdAsync(Guid id)
    {
        var role = (await _roleRepository.GetAsync(r => r.RoleID == id)).FirstOrDefault();
        return _mapper.Map<RoleResponse>(role);
    }

    public async Task<IEnumerable<RoleResponse>> GetByName(string name)
    {
        var roles = await _roleRepository.GetAsync(r => r.RoleName.Contains(name));
        return _mapper.Map<IEnumerable<RoleResponse>>(roles);
    }

    public async Task UpdateAsync(Guid id, UpdateRoleRequest updateModel)
    {
        var role = (await _roleRepository.GetAsync(r => r.RoleID == id)).FirstOrDefault();
        if (role == null)
        {
            throw new KeyNotFoundException("Role not found.");
        }

        var updatedRole = _mapper.Map(updateModel, role);
        await _roleRepository.UpdateAsync(updatedRole);
    }
}

