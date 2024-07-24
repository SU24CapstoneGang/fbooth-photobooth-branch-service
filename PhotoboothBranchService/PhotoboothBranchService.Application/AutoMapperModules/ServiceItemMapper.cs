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
            CreateMap<CreateServiceItemRequest, ServiceSession>().HandleNullProperty();
            CreateMap<UpdateServiceItemRequest, ServiceSession>().HandleNullProperty();
            CreateMap<ServiceSession, ServiceItemResponse>().HandleNullProperty();
            CreateMap<ServiceSession, CreateServiceItemResponse>().HandleNullProperty();
        }
    }
}
