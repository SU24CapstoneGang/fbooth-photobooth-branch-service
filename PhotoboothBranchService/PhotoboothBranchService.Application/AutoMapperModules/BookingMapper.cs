using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<BookingRequest, Booking>()
                .HandleNullProperty();
            CreateMap<UpdateSessionOrderRequest, Booking>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Booking, BookingResponse>().HandleNullProperty();
            CreateMap<Booking, CreateBookingResponse>().HandleNullProperty();
            CreateMap<CustomerBookingRequest, BookingRequest>().HandleNullProperty();
        }
    }
}
