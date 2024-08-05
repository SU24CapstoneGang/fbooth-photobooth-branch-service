﻿using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.BookingServices;

public interface IBookingService : IServiceBase<BookingResponse, SessionOrderFilter, PagingModel>
{
    Task<BookingResponse> Checkin(CheckinCodeRequest validateSessionOrderRequest);
    Task<CreateBookingResponse> CreateAsync(BookingRequest createModel, BookingType bookingType);
    Task<CreateBookingResponse> CustomerBooking(CustomerBookingRequest request, string email);
    Task<BookingResponse> GetByReferenceIDAsync(string id);
    Task<CreateBookingResponse> UpdateAsync(Guid id, UpdateBookingRequest updateModel, string? email);
    Task DeleteAsync(Guid id);
    Task<CancelBookingResponse> CancelBooking(Guid sessionOrdeID, string? ipAddress, string? email);
}
