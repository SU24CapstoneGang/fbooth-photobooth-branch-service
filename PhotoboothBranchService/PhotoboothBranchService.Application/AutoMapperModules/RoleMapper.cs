using AutoMapper;
using PhotoboothBranchService.Application.Common.Helper;
using PhotoboothBranchService.Application.DTOs.RequestModels.Role;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Role;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class RoleMapper : Profile
    {
        public RoleMapper() { 
            CreateMap<CreateRoleRequest,Role>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateRoleRequest,Role>().ReverseMap().HandleNullProperty();
            CreateMap<RoleResponse,Role>().ReverseMap().HandleNullProperty();
        }
    }
}
