using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.Services.BoothServices;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers;

public class BoothController : ControllerBaseApi
{
    private readonly IBoothService _boothService;

    public BoothController(IBoothService boothService)
    {
        _boothService = boothService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<CreateBoothResponse>> CreateBooth([FromBody]CreateBoothRequest createBoothRequest, BoothStatus status )
    {
        var createBoothResponse = await _boothService.CreateAsync(createBoothRequest, status);
        return Ok(createBoothResponse);
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoothResponse>>> GetAllBooths()
    {
        var booths = await _boothService.GetAllAsync();
        return Ok(booths);
    }

    [HttpGet("staff")]
    [Authorization("STAFF")]
    public async Task<ActionResult<IEnumerable<BoothResponse>>> StaffGetAllBooths()
    {
        var email = Request.HttpContext.Items["Email"]?.ToString();
        var booths = await _boothService.StaffGetAllAsync(email);
        return Ok(booths);
    }

    // Read with paging and filter
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<BoothResponse>>> GetAllBooths(
        [FromQuery] BoothFilter boothFilter, [FromQuery] PagingModel pagingModel)
    {
        var booths = await _boothService.GetAllPagingAsync(boothFilter, pagingModel);
        return Ok(booths);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<BoothResponse>>> GetBoothsByName(string name)
    {

        var booths = await _boothService.GetByName(name);
        return Ok(booths);

    }
    [HttpGet("active-booth")]
    public async Task<ActionResult<IEnumerable<BoothResponse>>> GetAvtiveBoothByTime([FromQuery]GetAvtiveBoothByTimeRequest request)
    {
        var booths = await _boothService.GetAvtiveBoothByTime(request);
        return Ok(booths);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BoothResponse>> GetBoothById(Guid id)
    {
        var booth = await _boothService.GetByIdAsync(id);
        if (booth == null)
        {
            return NotFound();
        }
        return Ok(booth);

    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBooth(Guid id, [FromBody] UpdateBoothRequest updateBoothRequest, [FromQuery] BoothStatus? status)
    {
        await _boothService.UpdateAsync(id, updateBoothRequest,status);
        return Ok();
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBooth(Guid id)
    {
        await _boothService.DeleteAsync(id);
        return Ok();
    }
}
