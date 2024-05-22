using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.MapSticker;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class MapStickerMapper : Profile
    {
        public MapStickerMapper() {
            CreateMap<CreateMapStickerRequest,MapSticker>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateMapStickerRequest, MapSticker>().ReverseMap().HandleNullProperty();
            CreateMap<MapSticker,MapStickerResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
