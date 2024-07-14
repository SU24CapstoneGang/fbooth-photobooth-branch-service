using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Application.Services.SessionOrderServices;

namespace PhotoboothBranchService.Api.Controllers;

public class SessionOrderController : ControllerBaseApi
{
    private readonly ISessionOrderService _sessionService;

    public SessionOrderController(ISessionOrderService sessionService)
    {
        _sessionService = sessionService;
    }

    // Create
    [HttpPost("staff")]
    public async Task<ActionResult<CreateSessionOrderResponse>> StaffCreateSession(CreateSessionOrderRequest createSessionRequest)
    {
        try
        {
            var createSessionOrderResponse = await _sessionService.CreateAsync(createSessionRequest);
            return Ok(createSessionOrderResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the session: {ex.Message}");
        }
    }
    [HttpPost("customer")]
    public async Task<ActionResult<CreateSessionOrderResponse>> CustomerCreateSession(CustomerBookingSessionOrderRequest customerBookingSessionOrderRequest)
    {
        try
        {
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var createSessionOrderResponse = await _sessionService.CustomerBooking(customerBookingSessionOrderRequest, email);
            return Ok(createSessionOrderResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the session: {ex.Message}");
        }
    }
    //validate code
    [HttpPost("validate")]
    public async Task<SessionOrderResponse> ValidateSessionOrder(ValidateSessionOrderRequest validateSessionPhotoRequest)
    {
        return await _sessionService.ValidateSessionOrder(validateSessionPhotoRequest);
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
    [HttpPost("cancel/{sessionOrderID}")]
    public async Task<ActionResult> CancelSession(Guid sessionOrderID)
    {
        try
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            await _sessionService.CancelSessionOrder(sessionOrderID, clientIp);
            return Ok();
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"An error occurred while cancel the session: {ex.Message}");
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

