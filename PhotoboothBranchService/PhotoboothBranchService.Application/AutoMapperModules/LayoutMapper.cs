using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.RequestModels.Layout;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Layout;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class LayoutMapper : Profile
    {
        public LayoutMapper() { 
            CreateMap<CreateLayoutRequest,Layout>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateLayoutRequest,Layout>().ReverseMap().HandleNullProperty();
            CreateMap<Layoutresponse, Layout>().ReverseMap().HandleNullProperty();
        }
    }
}
