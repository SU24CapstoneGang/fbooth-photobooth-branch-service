using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Filter;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class FilterMapper : Profile
    {
        public FilterMapper()
        {
            CreateMap<CreateFilterRequest, Filter>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateFilterRequest, Filter>().ReverseMap().HandleNullProperty();
            CreateMap<Filterresponse, Filter>().ReverseMap().HandleNullProperty();
        }
    }
}
