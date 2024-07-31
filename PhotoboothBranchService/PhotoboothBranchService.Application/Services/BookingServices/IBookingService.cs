using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BookingServices;

public interface IBookingService : IServiceBase<BookingResponse, SessionOrderFilter, PagingModel>
{
    Task<BookingResponse> ValidateBookingService(ValidateSessionOrderRequest validateSessionOrderRequest);
    Task<CreateBookingResponse> CreateAsync(BookingRequest createModel, BookingType bookingType);
    Task<CreateBookingResponse> CustomerBooking(CustomerBookingRequest request, string email);
    Task UpdateAsync(Guid id, UpdateSessionOrderRequest updateModel);
    Task DeleteAsync(Guid id);
    Task CancelSessionOrder(Guid sessionOrdeID, string? ipAddress);
}
