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

        var createSessionOrderResponse = await _sessionService.CreateAsync(createSessionRequest);
        return Ok(createSessionOrderResponse);
    }
    [HttpPost("customer")]
    public async Task<ActionResult<CreateSessionOrderResponse>> CustomerCreateSession(CustomerBookingSessionOrderRequest customerBookingSessionOrderRequest)
    {
        var email = Request.HttpContext.Items["Email"]?.ToString();
        var createSessionOrderResponse = await _sessionService.CustomerBooking(customerBookingSessionOrderRequest, email);
        return Ok(createSessionOrderResponse);
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
        var sessions = await _sessionService.GetAllAsync();
        return Ok(sessions);
    }
    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<SessionOrderResponse>>> GetAllSessions(
        [FromQuery] SessionOrderFilter sessionFilter, [FromQuery] PagingModel pagingModel)
    {
        var sessions = await _sessionService.GetAllPagingAsync(sessionFilter, pagingModel);
        return Ok(sessions);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<SessionOrderResponse>> GetSessionById(Guid id)
    {
        var session = await _sessionService.GetByIdAsync(id);
        if (session == null)
        {
            return NotFound();
        }
        return Ok(session);
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSession(Guid id, UpdateSessionOrderRequest updateSessionRequest)
    {
        await _sessionService.UpdateAsync(id, updateSessionRequest);
        return Ok();
    }
    [HttpPost("cancel")]
    public async Task<ActionResult> CancelSession(Guid sessionOrderID)
    {
        var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
        await _sessionService.CancelSessionOrder(sessionOrderID, clientIp);
        return Ok();
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSession(Guid id)
    {
        await _sessionService.DeleteAsync(id);
        return Ok();
    }
}

