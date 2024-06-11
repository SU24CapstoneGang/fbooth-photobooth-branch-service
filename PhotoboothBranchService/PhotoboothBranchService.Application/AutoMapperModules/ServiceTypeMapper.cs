using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServiceTypeMapper : Profile
    {
        public ServiceTypeMapper() 
        {
            CreateMap<CreateServiceTypeRequest, ServiceType>().HandleNullProperty();
            CreateMap<UpdateServiceTypeRequest, ServiceType>().HandleNullProperty();
            CreateMap<ServiceType, ServiceTypeResponse>().HandleNullProperty();
        }
    }
}
