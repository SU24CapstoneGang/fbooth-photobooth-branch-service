using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
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
    public async Task<ActionResult<Guid>> CreateSession(SessionDTO sessionDTO)
    {
        try
        {
            var id = await _sessionService.CreateAsync(sessionDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the session: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SessionDTO>>> GetAllSessions()
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

    [HttpGet("{id}")]
    public async Task<ActionResult<SessionDTO>> GetSessionById(Guid id)
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
    public async Task<ActionResult> UpdateSession(Guid id, SessionDTO sessionDTO)
    {
        try
        {
            await _sessionService.UpdateAsync(id, sessionDTO);
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

