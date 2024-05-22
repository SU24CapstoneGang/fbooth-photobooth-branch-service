using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Role;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Role;
using PhotoboothBranchService.Application.Services.RoleServices;

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
    public async Task<ActionResult<Guid>> CreateRole(CreateRoleRequest createRoleRequest)
    {
        try
        {
            var id = await _roleService.CreateAsync(createRoleRequest);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the role: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAllRoles()
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

    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAllRoles(
        [FromQuery] RoleFilter roleFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var roles = await _roleService.GetAllPagingAsync(roleFilter, pagingModel);
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving roles: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> GetRolesByName(string name)
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
    public async Task<ActionResult<RoleResponse>> GetRoleById(Guid id)
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
    public async Task<ActionResult> UpdateRole(Guid id, UpdateRoleRequest updateRoleRequest)
    {
        try
        {
            await _roleService.UpdateAsync(id, updateRoleRequest);
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
