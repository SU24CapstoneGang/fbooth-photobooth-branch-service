using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Constant;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ConstantMapper : Profile
    {
        public ConstantMapper() 
        {
            CreateMap<Constant, ConstantResponse>().HandleNullProperty();
            CreateMap<UpdateConstantRequest, Constant>().HandleNullProperty();
        }
    }
}
