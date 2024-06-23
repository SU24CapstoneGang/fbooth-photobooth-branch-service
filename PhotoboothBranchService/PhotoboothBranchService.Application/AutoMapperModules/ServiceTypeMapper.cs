using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServiceTypeMapper : Profile
    {
        public ServiceTypeMapper()
        {
            CreateMap<CreateServiceTypeRequest, ServiceType>().HandleNullProperty();
            CreateMap<UpdateServiceTypeRequest, ServiceType>().HandleNullProperty();
            CreateMap<ServiceType, ServiceTypeResponse>().HandleNullProperty();
            CreateMap<ServiceType, CreateServiceTypeResponse>().HandleNullProperty();
        }
    }
}
