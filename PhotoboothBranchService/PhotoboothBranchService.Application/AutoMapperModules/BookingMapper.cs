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
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => CombineDateAndTime(src.Date, src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => CombineDateAndTime(src.Date, src.EndTime)))
            .HandleNullProperty();
            CreateMap<UpdateBookingRequest, Booking>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => CombineDateAndTime(src.Date, src.StartTime)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => CombineDateAndTime(src.Date, src.EndTime)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Booking, BookingResponse>().HandleNullProperty();
            CreateMap<Booking, CreateBookingResponse>().HandleNullProperty();
            CreateMap<CustomerBookingRequest, BookingRequest>().HandleNullProperty();
        }
        private DateTime CombineDateAndTime(DateOnly dateOnly, TimeSpan timeSpan)
        {
            return new DateTime(
                dateOnly.Year,
                dateOnly.Month,
                dateOnly.Day,
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds
            );
        }
    }
}
