using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServicePackageMapper : Profile
    {
        public ServicePackageMapper()
        {
            CreateMap<CreateServiceRequest, ServicePackage>().HandleNullProperty();
            CreateMap<UpdateServiceRequest, ServicePackage>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ServicePackage, ServiceResponse>().HandleNullProperty();
            CreateMap<ServicePackage, CreateServiceResponse>().HandleNullProperty();
        }
    }
}
