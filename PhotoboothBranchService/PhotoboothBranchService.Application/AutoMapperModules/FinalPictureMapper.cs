using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class FinalPictureMapper : Profile
    {
        public FinalPictureMapper()
        {
            CreateMap<CreateFinalPictureRequest, FinalPicture>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateFinalPictureRequest,FinalPicture>().ReverseMap().HandleNullProperty();
            CreateMap<FinalPicture, FinalPictureResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
