using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<Guid> CreateAsync(RoleDTO entityDTO)
    {
        Role role = _mapper.Map<Role>(entityDTO);
        role.RoleID = Guid.NewGuid();
        await _roleRepository.AddAsync(role);
        return role.RoleID;
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var role = await _roleRepository.GetByIdAsync(id);
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

    public async Task<IEnumerable<RoleDTO>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAll();
        return _mapper.Map<IEnumerable<RoleDTO>>(roles);
    }

    public async Task<RoleDTO> GetByIdAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        return _mapper.Map<RoleDTO>(role);
    }

    public async Task<IEnumerable<RoleDTO>> GetByName(string name)
    {
        var roles = await _roleRepository.GetByName(name);
        return _mapper.Map<IEnumerable<RoleDTO>>(roles);
    }

    public async Task UpdateAsync(Guid id, RoleDTO entityDTO)
    {
        entityDTO.RoleID = id;
        Role role = _mapper.Map<Role>(entityDTO);
        await _roleRepository.UpdateAsync(role);
    }
}

