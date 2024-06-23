using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BackgroundMapper : Profile
    {
        public BackgroundMapper()
        {
            CreateMap<CreateBackgroundRequest, Background>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateBackgroundRequest, Background>().ReverseMap().HandleNullProperty();
            CreateMap<BackgroundResponse, Background>().ReverseMap().HandleNullProperty();
            CreateMap<Background, CreateBackgroundResponse>().HandleNullProperty();
        }
    }
}
