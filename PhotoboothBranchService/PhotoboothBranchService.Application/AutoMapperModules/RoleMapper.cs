using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Role;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<CreateRoleRequest, Role>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateRoleRequest, Role>().ReverseMap().HandleNullProperty();
            CreateMap<RoleResponse, Role>().ReverseMap().HandleNullProperty();
        }
    }
}
