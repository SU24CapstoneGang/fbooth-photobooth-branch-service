using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Session;
using PhotoboothBranchService.Application.Services.SessionServices;

namespace PhotoboothBranchService.Api.Controllers;

public class SessionOrderController : ControllerBaseApi
{
    private readonly ISessionOrderService _sessionService;

    public SessionOrderController(ISessionOrderService sessionService)
    {
        _sessionService = sessionService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateSession(CreateSessionOrderRequest createSessionRequest)
    {
        try
        {
            var id = await _sessionService.CreateAsync(createSessionRequest);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the session: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SessionOrderResponse>>> GetAllSessions()
    {
        try
        {
            var sessions = await _sessionService.GetAllAsync();
            return Ok(sessions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving sessions: {ex.Message}");
        }
    }
    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<SessionOrderResponse>>> GetAllSessions(
        [FromQuery] SessionOrderFilter sessionFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var sessions = await _sessionService.GetAllPagingAsync(sessionFilter, pagingModel);
            return Ok(sessions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving sessions: {ex.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<SessionOrderResponse>> GetSessionById(Guid id)
    {
        try
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the session by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSession(Guid id, UpdateSessionOrderRequest updateSessionRequest)
    {
        try
        {
            await _sessionService.UpdateAsync(id, updateSessionRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the session: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSession(Guid id)
    {
        try
        {
            await _sessionService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the session: {ex.Message}");
        }
    }
}

