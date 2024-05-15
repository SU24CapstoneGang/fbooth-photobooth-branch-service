using PhotoboothBranchService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
    public async Task<ActionResult<Guid>> CreateRole(RoleDTO roleDTO, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _roleService.CreateAsync(roleDTO, cancellationToken);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the role: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles(CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _roleService.GetAllAsync(cancellationToken);
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving roles: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRolesByName(string name, CancellationToken cancellationToken)
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
    public async Task<ActionResult<RoleDTO>> GetRoleById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleService.GetByIdAsync(id, cancellationToken);
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
    public async Task<ActionResult> UpdateRole(Guid id, RoleDTO roleDTO, CancellationToken cancellationToken)
    {
        try
        {
            await _roleService.UpdateAsync(id, roleDTO, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the role: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _roleService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the role: {ex.Message}");
        }
    }
}
