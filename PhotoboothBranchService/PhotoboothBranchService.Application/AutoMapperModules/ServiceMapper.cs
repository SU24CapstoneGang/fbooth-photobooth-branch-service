using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<CreateServiceRequest, Service>().HandleNullProperty();
            CreateMap<UpdateServiceRequest, Service>()
              .ForMember(dest => dest.ServiceName, opt => opt.MapFrom((src, dest) => src.ServiceName ?? dest.ServiceName))
              .ForMember(dest => dest.ServiceDescription, opt => opt.MapFrom((src, dest) => src.ServiceDescription ?? dest.ServiceDescription))
              .ForMember(dest => dest.Unit, opt => opt.MapFrom((src, dest) => src.Unit ?? dest.Unit))
              .ForMember(dest => dest.ServicePrice, opt => opt.MapFrom((src, dest) => src.ServicePrice.HasValue ? src.ServicePrice.Value : dest.ServicePrice))
              .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Status.HasValue ? src.Status.Value : dest.Status))
              .ForMember(dest => dest.ServiceIamgeURL, opt => opt.Ignore()) // Assuming ServiceIamgeURL is not updated via this request
              .ForMember(dest => dest.CouldID, opt => opt.Ignore()) // Assuming CouldID is managed separately
              .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Typically set during creation
              .ForMember(dest => dest.LastModified, opt => opt.Ignore()) // Typically updated during update
              .ForMember(dest => dest.ServiceType, opt => opt.MapFrom((src, dest) => src.ServiceType.HasValue ? (ServiceType)src.ServiceType.Value : dest.ServiceType));

            CreateMap<Service, ServiceResponse>().HandleNullProperty();
            CreateMap<Service, CreateServiceResponse>().HandleNullProperty();
        }
    }
}
