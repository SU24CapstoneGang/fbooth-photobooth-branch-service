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
            CreateMap<CreateServiceItemRequest, ServiceItem>().HandleNullProperty();
            CreateMap<UpdateServiceItemRequest, ServiceItem>().HandleNullProperty();
            CreateMap<ServiceItem, ServiceItemResponse>().HandleNullProperty();
            CreateMap<ServiceItem, CreateServiceItemResponse>().HandleNullProperty();
        }
    }
}
