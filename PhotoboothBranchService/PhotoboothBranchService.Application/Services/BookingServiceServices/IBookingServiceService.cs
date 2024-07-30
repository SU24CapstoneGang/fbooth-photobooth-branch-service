using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.BookingServiceServices
{
    public interface IBookingServiceService : IServiceBase<BookingServiceResponse, BookingServiceFilter, PagingModel>
    {
        Task<AddListBookingServiceResponse> AddListServiceItem(AddListBookingServiceRequest request);
        Task<CreateBookingServiceResponse> CreateAsync(CreateBookingServiceRequest createModel);
        Task UpdateAsync(Guid id, UpdateBookingServiceRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
