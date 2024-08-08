using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.Services.BookingServices;
using PhotoboothBranchService.Domain.Enum;

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
            var createBookingResponse = await _bookingService.CreateAsync(bookingRequest, BookingType.Staff);
            return Ok(createBookingResponse);
        }
        [HttpPost("customer-booking")]
        public async Task<ActionResult<CreateBookingResponse>> CustomerCreateSession(CustomerBookingRequest customerBookingRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var createBookingResponse = await _bookingService.CustomerBooking(customerBookingRequest, email);
            return Ok(createBookingResponse);
        }
        [HttpPost("checkin-booking")]
        public async Task<ActionResult<BookingResponse>> Checkin([FromBody] CheckinCodeRequest request)
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
            [FromQuery] BookingFilter sessionFilter, [FromQuery] PagingModel pagingModel)
        {
            var sessions = await _bookingService.GetAllPagingAsync(sessionFilter, pagingModel);
            return Ok(sessions);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponse>> GetBookingById(Guid id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
        [HttpGet("rid/{id}")]
        public async Task<ActionResult<BookingResponse>> GetBookingByReferenceID(string id)
        {
            var booking = await _bookingService.GetByReferenceIDAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult<CreateBookingResponse>> UpdateBooking(Guid id, UpdateBookingRequest updateBookingRequest)
        {
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var response = await _bookingService.UpdateAsync(id, updateBookingRequest, email);
            return Ok(response);
        }
        [HttpPost("cancel")]
        public async Task<ActionResult<CancelBookingResponse>> CancelBooking(Guid BookingID)
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var response = await _bookingService.CancelBooking(BookingID, clientIp, email);
            return Ok(response);
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

