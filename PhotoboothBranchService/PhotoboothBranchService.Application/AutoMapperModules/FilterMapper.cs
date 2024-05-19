using AutoMapper;
using PhotoboothBranchService.Application.Common.Helper;
using PhotoboothBranchService.Application.DTOs.RequestModels.Filter;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Filter;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class FilterMapper : Profile
    {
        public FilterMapper()
        {
            CreateMap<CreateFilterRequest,Filter>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateFilterRequest, Filter>().ReverseMap().HandleNullProperty();
            CreateMap<Filterresponse, Filter>().ReverseMap().HandleNullProperty();
        }
    }
}
