using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<CreateServiceRequest, Service>().HandleNullProperty();
            CreateMap<UpdateServiceRequest, Service>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Service, ServiceResponse>().HandleNullProperty();
            CreateMap<Service, CreateServiceResponse>().HandleNullProperty();
        }
    }
}
