using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper() {
            CreateMap<CreateServiceRequest, Service>().HandleNullProperty();
            CreateMap<UpdateServiceRequest, Service>().HandleNullProperty();
            CreateMap<Service, ServiceResponse>().HandleNullProperty();
        }
    }
}
