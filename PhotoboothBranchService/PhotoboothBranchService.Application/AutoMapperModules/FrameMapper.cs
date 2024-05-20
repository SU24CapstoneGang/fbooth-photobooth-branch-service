using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.RequestModels.Frame;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Frame;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class FrameMapper : Profile
    {
        public FrameMapper()
        {
            CreateMap<CreateFrameRequest, Frame>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateFrameRequest, Frame>().ReverseMap().HandleNullProperty();
            CreateMap<FrameResponse, Frame>().ReverseMap().HandleNullProperty();
        }
    }
}
