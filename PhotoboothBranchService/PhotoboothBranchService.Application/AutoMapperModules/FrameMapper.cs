using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Frame;
using PhotoboothBranchService.Domain.Entities;

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
