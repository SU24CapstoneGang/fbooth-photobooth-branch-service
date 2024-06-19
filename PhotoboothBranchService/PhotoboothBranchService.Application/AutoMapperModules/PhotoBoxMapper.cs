using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoBoxMapper : Profile
    {
        public PhotoBoxMapper()
        {
            CreateMap<CreatePhotoBoxRequest, PhotoBox>().HandleNullProperty();
            CreateMap<UpdatePhotoBoxRequest, PhotoBox>().HandleNullProperty();
            CreateMap<PhotoBox, PhotoBoxResponse>().HandleNullProperty();
        }
    }
}
