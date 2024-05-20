using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.RequestModels.Sticker;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Sticker;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class StickerMapper : Profile
    {
        public StickerMapper() { 
            CreateMap<CreateStickerRequest,Sticker>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateStickerRequest,Sticker>().ReverseMap().HandleNullProperty();
            CreateMap<StickerResponse,Sticker>().ReverseMap().HandleNullProperty();
        }
    }
}
