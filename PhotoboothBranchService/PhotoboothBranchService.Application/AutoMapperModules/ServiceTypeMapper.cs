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
            CreateMap<CreateServiceTypeRequest, Service>().HandleNullProperty();
            CreateMap<UpdateServiceTypeRequest, Service>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Service, ServiceTypeResponse>().HandleNullProperty();
            CreateMap<Service, CreateServiceTypeResponse>().HandleNullProperty();
        }
    }
}
