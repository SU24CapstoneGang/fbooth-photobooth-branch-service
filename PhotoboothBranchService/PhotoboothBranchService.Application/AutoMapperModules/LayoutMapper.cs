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
            CreateMap<UpdateLayoutRequest, Layout>()
    .ForMember(dest => dest.LayoutCode, opt => opt.MapFrom((src, dest) => src.LayoutCode ?? dest.LayoutCode))
    .ForMember(dest => dest.LayoutURL, opt => opt.MapFrom((src, dest) => src.LayoutURL ?? dest.LayoutURL))
    .ForMember(dest => dest.CouldID, opt => opt.MapFrom((src, dest) => src.CouldID ?? dest.CouldID))
    .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Status.HasValue ? src.Status.Value : dest.Status))
    .ForMember(dest => dest.Height, opt => opt.MapFrom((src, dest) => src.Height.HasValue ? src.Height.Value : dest.Height))
    .ForMember(dest => dest.Width, opt => opt.MapFrom((src, dest) => src.Width.HasValue ? src.Width.Value : dest.Width))
    .ForMember(dest => dest.PhotoSlot, opt => opt.MapFrom((src, dest) => src.PhotoSlot.HasValue ? src.PhotoSlot.Value : dest.PhotoSlot))
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Typically set by creation logic
    .ForMember(dest => dest.LastModified, opt => opt.Ignore()); // Typically set by update logic
            CreateMap<Layout, LayoutResponse>().HandleNullProperty();
            CreateMap<Layout, CreateLayoutResponse>().HandleNullProperty();
            CreateMap<Layout, LayoutSummaryResponse>().HandleNullProperty();
        }
    }
}
