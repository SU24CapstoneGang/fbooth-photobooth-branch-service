﻿using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<CreateServiceRequest, ServicePackage>().HandleNullProperty();
            CreateMap<UpdateServiceRequest, ServicePackage>().HandleNullProperty();
            CreateMap<ServicePackage, ServiceResponse>().HandleNullProperty();
            CreateMap<ServicePackage, CreateServiceResponse>().HandleNullProperty();
        }
    }
}
