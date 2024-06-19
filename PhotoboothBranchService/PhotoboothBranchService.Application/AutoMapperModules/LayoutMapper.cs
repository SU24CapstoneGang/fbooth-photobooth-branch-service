using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class LayoutMapper : Profile
    {
        public LayoutMapper()
        {
            CreateMap<CreateLayoutRequest, Layout>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateLayoutRequest, Layout>().ReverseMap().HandleNullProperty();
            CreateMap<LayoutResponse, Layout>().ReverseMap().HandleNullProperty();
        }
    }
}
