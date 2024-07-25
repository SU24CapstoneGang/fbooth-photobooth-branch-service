using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServiceItemMapper : Profile
    {
        public ServiceItemMapper()
        {
            CreateMap<CreateServiceItemRequest, BookingService>().HandleNullProperty();
            CreateMap<UpdateServiceItemRequest, BookingService>().HandleNullProperty();
            CreateMap<BookingService, ServiceItemResponse>().HandleNullProperty();
            CreateMap<BookingService, CreateServiceItemResponse>().HandleNullProperty();
        }
    }
}
