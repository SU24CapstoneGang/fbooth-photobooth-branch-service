using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BookingService;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.Services.BookingServiceServices
{
    public interface IBookingServiceService : IServiceBase<BookingServiceResponse, BookingServiceFilter, PagingModel>
    {
        Task<decimal> AddExtraService(AddListBookingServiceRequest request);
        Task<CreateBookingServiceResponse> CreateAsync(CreateBookingServiceRequest createModel);
        Task UpdateAsync(Guid id, UpdateBookingServiceRequest updateModel);
        Task DeleteAsync(Guid id);
        Task DeleteByBookingIdAsync(Guid BookingID);
        Task<(decimal, ICollection<BookingService>)> CreateServiceListForNewBooking(Dictionary<Guid, short> serviceList);
        Task<IEnumerable<BookingService>> GetByBookingIdAsync(Guid BookingID);
        Task AddByList(List<BookingService> resquestList, Guid bookingID);
    }
}
