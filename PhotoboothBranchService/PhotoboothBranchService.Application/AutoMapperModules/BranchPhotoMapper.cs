using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.BranchPhoto;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BranchPhotoMapper : Profile
    {
        public BranchPhotoMapper()
        {
            CreateMap<BranchPhotoResponse, BranchPhoto>().HandleNullProperty().ReverseMap();
        }

    }
}
