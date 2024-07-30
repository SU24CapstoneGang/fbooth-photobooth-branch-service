using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BookingServiceMapper : Profile
    {
        public BookingServiceMapper()
        {
            CreateMap<CreateBookingServiceRequest, BookingService>().HandleNullProperty();
            CreateMap<UpdateBookingServiceRequest, BookingService>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BookingService, BookingServiceResponse>().HandleNullProperty();
            CreateMap<BookingService, CreateBookingServiceResponse>().HandleNullProperty();
        }
    }
}
