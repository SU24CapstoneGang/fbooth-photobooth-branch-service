using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Session;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Session;
using PhotoboothBranchService.Application.Services.SessionServices;

namespace PhotoboothBranchService.Api.Controllers;

public class SessionController : ControllerBaseApi
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateSession(CreateSessionRequest createSessionRequest)
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
    public async Task<ActionResult<IEnumerable<SessionResponse>>> GetAllSessions()
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
    public async Task<ActionResult<IEnumerable<SessionResponse>>> GetAllSessions(
        [FromQuery] SessionFilter sessionFilter, [FromQuery] PagingModel pagingModel)
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
    public async Task<ActionResult<SessionResponse>> GetSessionById(Guid id)
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
    public async Task<ActionResult> UpdateSession(Guid id, UpdateSessionRequest updateSessionRequest)
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

