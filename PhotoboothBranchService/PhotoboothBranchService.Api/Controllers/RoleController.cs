using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PhotoboothBranchService.Application.Services.RoleServices;
using PhotoboothBranchService.Application.DTOs;

namespace PhotoboothBranchService.Api.Controllers;

public class RoleController : ControllerBaseApi
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRole(RoleDTO roleDTO)
    {
        try
        {
            var id = await _roleService.CreateAsync(roleDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the role: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles()
    {
        try
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving roles: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRolesByName(string name)
    {
        try
        {
            var roles = await _roleService.GetByName(name);
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving roles by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDTO>> GetRoleById(Guid id)
    {
        try
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the role by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRole(Guid id, RoleDTO roleDTO)
    {
        try
        {
            await _roleService.UpdateAsync(id, roleDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the role: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRole(Guid id)
    {
        try
        {
            await _roleService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the role: {ex.Message}");
        }
    }
}
