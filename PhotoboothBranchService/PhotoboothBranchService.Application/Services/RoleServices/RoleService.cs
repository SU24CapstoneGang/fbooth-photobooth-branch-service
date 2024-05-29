using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Role;
using PhotoboothBranchService.Domain.Common.Helper;
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
        try
        {
            var isRoleExisted = (await _roleRepository.GetAsync(r => r.RoleName.Trim().Replace(" ", "")
                                    .Equals(createModel.RoleName.Trim().Replace(" ", "")))).FirstOrDefault();
            if (isRoleExisted != null)
                throw new BadRequestException("Role name is already existed");

            Role role = _mapper.Map<Role>(createModel);
            role.RoleName = createModel.RoleName.Trim().Replace(" ", "");

            await _roleRepository.AddAsync(role);
            return role.RoleID;

        } catch (Exception ex)
        {
            throw new Exception("An error occurred while create role: " + ex.Message);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var role = (await _roleRepository.GetAsync(r => r.RoleID == id)).FirstOrDefault();
            if (role == null)
                throw new NotFoundException("Role", id, "Role not found");
            
            await _roleRepository.RemoveAsync(role);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting role: " + ex.Message);
        }
    }

    public async Task<IEnumerable<RoleResponse>> GetAllAsync()
    {
        try
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleResponse>>(roles.ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting role: " + ex.Message);
        }
    }

    public async Task<IEnumerable<RoleResponse>> GetAllPagingAsync(RoleFilter filter, PagingModel paging)
    {
        try
        {
            var roles = (await _roleRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listRoleresponse = _mapper.Map<IEnumerable<RoleResponse>>(roles);
            listRoleresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listRoleresponse;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting role: " + ex.Message);
        }
    }

    public async Task<RoleResponse> GetByIdAsync(Guid id)
    {
        try
        {
            var role = (await _roleRepository.GetAsync(r => r.RoleID == id)).FirstOrDefault();
            return _mapper.Map<RoleResponse>(role);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting role: " + ex.Message);
        }
    }

    public async Task<IEnumerable<RoleResponse>> GetByName(string name)
    {
        try
        {
            var roles = await _roleRepository.GetAsync(r => r.RoleName.Contains(name));
            return _mapper.Map<IEnumerable<RoleResponse>>(roles);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting role: " + ex.Message);
        }
    }

    public async Task UpdateAsync(Guid id, UpdateRoleRequest updateModel)
    {
        try
        {
            var role = (await _roleRepository.GetAsync(r => r.RoleID == id)).FirstOrDefault();
            if (role == null)
                throw new NotFoundException("Role", id, "Role ID not found");

            var updatedRole = _mapper.Map(updateModel, role);
            await _roleRepository.UpdateAsync(updatedRole);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while update role: " + ex.Message);
        }
    }
}

