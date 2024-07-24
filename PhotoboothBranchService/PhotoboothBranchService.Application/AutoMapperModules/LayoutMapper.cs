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
            CreateMap<CreateLayoutRequest, Layout>().HandleNullProperty();
            CreateMap<UpdateLayoutRequest, Layout>().HandleNullProperty();
            CreateMap<Layout, LayoutResponse>().HandleNullProperty();
            CreateMap<Layout, CreateLayoutResponse>().HandleNullProperty();
            CreateMap<Layout, LayoutSummaryResponse>().HandleNullProperty();
        }
    }
}
