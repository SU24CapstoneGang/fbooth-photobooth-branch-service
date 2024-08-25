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
    public async Task<ActionResult<CreateBoothResponse>> CreateBooth([FromBody]CreateBoothRequest createBoothRequest)
    {
        var createBoothResponse = await _boothService.CreateAsync(createBoothRequest);
        return Ok(createBoothResponse);
    }

    [HttpPost("{boothID}/photos")]
    public async Task<IActionResult> AddPhotoForBooth(Guid boothID, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file provided.");
        }

        try
        {
            var response = await _boothService.AddPhotoForBooth(boothID, file);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost("active/{code}")]
    public async Task<ActionResult<BoothResponse>> ActiveBooth(string code)
    {
        var response = await _boothService.ActiveBooth(code);
        return Ok(response);
    }
    // Read
    [HttpGet]
    [Authorization("ADMIN")]
    public async Task<ActionResult<IEnumerable<AdminBoothResponse>>> GetAllBooths()
    {
        var booths = await _boothService.AdminGetAllAsync();
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
    public async Task<ActionResult<IEnumerable<BoothResponse>>> CustomerGetAll()
    {
        var booths = await _boothService.CustomerGetAllAsync();
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
    public async Task<ActionResult> UpdateBooth(Guid id, [FromBody] UpdateBoothRequest updateBoothRequest)
    {
        await _boothService.UpdateAsync(id, updateBoothRequest);
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
