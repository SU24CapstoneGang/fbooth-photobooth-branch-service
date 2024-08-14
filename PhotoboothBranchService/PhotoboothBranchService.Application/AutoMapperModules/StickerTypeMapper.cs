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
            CreateMap<UpdateStickerTypeRequest, StickerType>().HandleNullProperty();
        }
    }
}
