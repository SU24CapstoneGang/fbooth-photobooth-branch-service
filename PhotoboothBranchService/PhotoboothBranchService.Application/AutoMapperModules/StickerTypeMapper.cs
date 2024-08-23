using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.StickerType;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class StickerTypeMapper : Profile
    {
        public StickerTypeMapper() 
        {
            CreateMap<StickerType, StickerTypeResponse>().HandleNullProperty();
            CreateMap<CreateStickerTypeRequest, StickerType>().HandleNullProperty();
            CreateMap<UpdateStickerTypeRequest, StickerType>()
    .ForMember(dest => dest.StickerTypeName, opt => opt.MapFrom((src, dest) => src.StickerTypeName ?? dest.StickerTypeName))
    .ForMember(dest => dest.RepresentImageURL, opt => opt.Ignore()) // This property is not in the request, so it should be left unchanged.
    .ForMember(dest => dest.CouldID, opt => opt.Ignore()) // This property is not in the request, so it should be left unchanged.
    .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Status.HasValue ? src.Status.Value : dest.Status))
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Typically set during creation
    .ForMember(dest => dest.LastModified, opt => opt.Ignore()); // Typically updated during update

        }
    }
}
