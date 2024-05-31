using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.EffectsPackLog;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class EffectsPackLogMapper : Profile
    {
        public EffectsPackLogMapper()
        {
            CreateMap<CreateEffectsPackLogRequest, EffectsPackLog>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateEffectsPackLogRequest, EffectsPackLog>().ReverseMap().HandleNullProperty();
            CreateMap<EffectsPackLog, EffectsPackLogResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
