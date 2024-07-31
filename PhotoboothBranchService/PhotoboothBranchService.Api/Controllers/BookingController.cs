using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.Services.BookingServices;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers
{

    public class BookingController : ControllerBaseApi
    {
        private readonly IBookingService _sessionService;

        public BookingController(IBookingService sessionService)
        {
            _sessionService = sessionService;
        }

        // Create
        [HttpPost("staff-create")]
        public async Task<ActionResult<CreateBookingResponse>> StaffCreateSession(BookingRequest bookingRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createBookingResponse = await _sessionService.CreateAsync(bookingRequest, BookingType.Staff);
            return Ok(createBookingResponse);
        }
        [HttpPost("customer-booking")]
        public async Task<ActionResult<CreateBookingResponse>> CustomerCreateSession(CustomerBookingRequest customerBookingSessionOrderRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var createBookingResponse = await _sessionService.CustomerBooking(customerBookingSessionOrderRequest, email);
            return Ok(createBookingResponse);
        }
        //validate code
        [HttpPost("validate")]
        public async Task<BookingResponse> ValidateSessionOrder(ValidateSessionOrderRequest validateSessionPhotoRequest)
        {
            return await _sessionService.ValidateBookingService(validateSessionPhotoRequest);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetAllSessions()
        {
            var sessions = await _sessionService.GetAllAsync();
            return Ok(sessions);
        }
        //get all with filter and paging
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetAllBooking(
            [FromQuery] SessionOrderFilter sessionFilter, [FromQuery] PagingModel pagingModel)
        {
            var sessions = await _sessionService.GetAllPagingAsync(sessionFilter, pagingModel);
            return Ok(sessions);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponse>> GetBookingById(Guid id)
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
        public async Task<ActionResult> UpdateBooking(Guid id, UpdateSessionOrderRequest updateSessionRequest)
        {
            await _sessionService.UpdateAsync(id, updateSessionRequest);
            return Ok();
        }
        [HttpPost("cancel")]
        public async Task<ActionResult> CancelBooking(Guid sessionOrderID)
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            await _sessionService.CancelSessionOrder(sessionOrderID, clientIp);
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(Guid id)
        {
            await _sessionService.DeleteAsync(id);
            return Ok();
        }
    }
}

