using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BoothMapper : Profile
    {
        public BoothMapper()
        {
            CreateMap<CreateBoothRequest, Booth>().HandleNullProperty();
            CreateMap<UpdateBoothRequest, Booth>().HandleNullProperty();
            CreateMap<Booth, BoothResponse>().HandleNullProperty();
            CreateMap<Booth, CreateBoothResponse>().HandleNullProperty();
        }
    }
}
