using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ServiceItemMapper : Profile
    {
        public ServiceItemMapper() 
        {
            CreateMap<CreateServiceItemRequest, ServiceItem>().HandleNullProperty();
            CreateMap<UpdateServiceItemRequest, ServiceItem>().HandleNullProperty();
            CreateMap<ServiceItem, ServiceItemResponse>().HandleNullProperty();
        }
    }
}
