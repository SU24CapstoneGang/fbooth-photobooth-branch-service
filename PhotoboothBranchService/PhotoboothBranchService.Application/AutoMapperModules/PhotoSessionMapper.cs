using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoSessionMapper : Profile
    {
        public PhotoSessionMapper() 
        {
            CreateMap<CreatePhotoSessionRequest, PhotoSession>().HandleNullProperty();
            CreateMap<UpdatePhotoSessionRequest, PhotoSession>().HandleNullProperty();
            CreateMap<PhotoSession, PhotoSessionResponse>().HandleNullProperty();
        }
    }
}
