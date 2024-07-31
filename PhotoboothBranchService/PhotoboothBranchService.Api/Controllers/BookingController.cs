using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.Services.BookingServices;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Api.Controllers
{

    public class BookingController : ControllerBaseApi
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Create
        [HttpPost("staff-create")]
        public async Task<ActionResult<CreateBookingResponse>> StaffCreateSession(BookingRequest bookingRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createBookingResponse = await _bookingService.CreateAsync(bookingRequest);
            return Ok(createBookingResponse);
        }
        [HttpPost("customer-booking")]
        public async Task<ActionResult<CreateBookingResponse>> CustomerCreateSession(CustomerBookingSessionOrderRequest customerBookingSessionOrderRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var createBookingResponse = await _bookingService.CustomerBooking(customerBookingSessionOrderRequest, email);
            return Ok(createBookingResponse);
        }
        [HttpPost("checkin-booking")]
        public async Task<IActionResult> Checkin([FromBody] CheckinCodeRequest request)
        {
            try
            {
                var response = await _bookingService.Checkin(request);
                return Ok(response);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetAllSessions()
        {
            var sessions = await _bookingService.GetAllAsync();
            return Ok(sessions);
        }
        //get all with filter and paging
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetAllBooking(
            [FromQuery] SessionOrderFilter sessionFilter, [FromQuery] PagingModel pagingModel)
        {
            var sessions = await _bookingService.GetAllPagingAsync(sessionFilter, pagingModel);
            return Ok(sessions);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponse>> GetBookingById(Guid id)
        {
            var session = await _bookingService.GetByIdAsync(id);
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
            await _bookingService.UpdateAsync(id, updateSessionRequest);
            return Ok();
        }
        [HttpPost("cancel")]
        public async Task<ActionResult> CancelBooking(Guid sessionOrderID)
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            await _bookingService.CancelSessionOrder(sessionOrderID, clientIp);
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(Guid id)
        {
            await _bookingService.DeleteAsync(id);
            return Ok();
        }
    }
}

