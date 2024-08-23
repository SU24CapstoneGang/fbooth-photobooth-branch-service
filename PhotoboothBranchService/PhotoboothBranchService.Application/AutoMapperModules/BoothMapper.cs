using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Entities;
using System.Collections;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BoothMapper : Profile
    {
        public BoothMapper()
        {
            CreateMap<CreateBoothRequest, Booth>().HandleNullProperty();
            CreateMap<UpdateBoothRequest, Booth>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom((source, dest) => source.Status ?? dest.Status))
                .ForMember(dest => dest.PeopleInBooth, opt => opt.MapFrom((source, dest) => source.PeopleInBooth ?? dest.PeopleInBooth))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Booth, BoothResponse>().HandleNullProperty();
            CreateMap<Booth, CreateBoothResponse>().HandleNullProperty();
        }
    }
}
