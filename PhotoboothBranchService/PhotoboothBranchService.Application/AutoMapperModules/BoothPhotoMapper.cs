using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.DTOs.BoothPhoto;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BoothPhotoMapper : Profile
    {
        public BoothPhotoMapper() 
        {
            CreateMap<BoothPhotoResponse, BoothPhoto>().HandleNullProperty().ReverseMap();
        }
    }
}
