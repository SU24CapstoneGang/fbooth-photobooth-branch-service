using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BookingServices;

public interface IBookingService : IServiceBase<BookingResponse, BookingFilter, PagingModel>
{
    Task<BookingResponse> Checkin(CheckinCodeRequest validateSessionOrderRequest);
    Task<CreateBookingResponse> CreateAsync(BookingRequest createModel, BookingType bookingType);
    Task<CreateBookingResponse> GuestBooking(GuestBookingRequest request);
    Task<BookingResponse> AddExtraService(AddExtraServiceRequest request);
    Task<CreateBookingResponse> CustomerBooking(CustomerBookingRequest request, string email);
    Task<IEnumerable<BookingResponse>> SearchByReferenceIDAsync(string id);
    Task<CreateBookingResponse> UpdateAsync(Guid id, UpdateBookingRequest updateModel, string? email);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<BookingResponse>> GetBranchFutureBooking(Guid branchID);
    Task<IEnumerable<BookingResponse>> GetBoothFutureBooking(Guid boothID);
    Task<CancelBookingResponse> CancelBooking(Guid sessionOrdeID, string? ipAddress, string? email);
}
