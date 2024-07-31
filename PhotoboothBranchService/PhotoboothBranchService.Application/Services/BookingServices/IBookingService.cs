using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.BookingServices;

public interface IBookingService : IServiceBase<BookingResponse, SessionOrderFilter, PagingModel>
{
    Task<BookingResponse> Checkin(CheckinCodeRequest validateSessionOrderRequest);
    Task<CreateBookingResponse> CreateAsync(BookingRequest createModel);
    Task<CreateBookingResponse> CustomerBooking(CustomerBookingSessionOrderRequest request, string email);
    Task UpdateAsync(Guid id, UpdateSessionOrderRequest updateModel);
    Task DeleteAsync(Guid id);
    Task CancelSessionOrder(Guid sessionOrdeID, string? ipAddress);
}
